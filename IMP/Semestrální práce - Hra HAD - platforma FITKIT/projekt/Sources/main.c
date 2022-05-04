/* Copyright (c) 2015, Freescale Semiconductor, Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 *
 * o Redistributions of source code must retain the above copyright notice, this list
 *   of conditions and the following disclaimer.
 *
 * o Redistributions in binary form must reproduce the above copyright notice, this
 *   list of conditions and the following disclaimer in the documentation and/or
 *   other materials provided with the distribution.
 *
 * o Neither the name of Freescale Semiconductor, Inc. nor the names of its
 *   contributors may be used to endorse or promote products derived from this
 *   software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
 * ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#include "MK60D10.h"

/* Macros for bit-level registers manipulation */
#define GPIO_PIN_MASK	0x1Fu
#define GPIO_PIN(x)		(((1)<<(x & GPIO_PIN_MASK)))

#define BUTTON_UP 0b100000000000000000000000000
#define BUTTON_LEFT 0b1000000000000000000000000000
#define BUTTON_DOWN 0b1000000000000
#define BUTTON_RIGHT 0b10000000000

#define CLEAR_ROWS 0b000000000000000000000000000000

struct snake{
	int x;
	int y;
	int direction;
};

#define SNAKE_LEN 3
struct snake snake[SNAKE_LEN];

void column_select(unsigned int col_num);


/* Configuration of the necessary MCU peripherals */
void SystemConfig() {
	/* Turn on all port clocks */
	SIM->SCGC5 = SIM_SCGC5_PORTA_MASK | SIM_SCGC5_PORTE_MASK;


	/* Set corresponding PTA pins (column activators of 74HC154) for GPIO functionality */
	PORTA->PCR[8] = ( 0|PORT_PCR_MUX(0x01) );  // A0
	PORTA->PCR[10] = ( 0|PORT_PCR_MUX(0x01) ); // A1
	PORTA->PCR[6] = ( 0|PORT_PCR_MUX(0x01) );  // A2
	PORTA->PCR[11] = ( 0|PORT_PCR_MUX(0x01) ); // A3

	/* Set corresponding PTA pins (rows selectors of 74HC154) for GPIO functionality */
	PORTA->PCR[26] = ( 0|PORT_PCR_MUX(0x01) );  // R0
	PORTA->PCR[24] = ( 0|PORT_PCR_MUX(0x01) );  // R1
	PORTA->PCR[9] = ( 0|PORT_PCR_MUX(0x01) );   // R2
	PORTA->PCR[25] = ( 0|PORT_PCR_MUX(0x01) );  // R3
	PORTA->PCR[28] = ( 0|PORT_PCR_MUX(0x01) );  // R4
	PORTA->PCR[7] = ( 0|PORT_PCR_MUX(0x01) );   // R5
	PORTA->PCR[27] = ( 0|PORT_PCR_MUX(0x01) );  // R6
	PORTA->PCR[29] = ( 0|PORT_PCR_MUX(0x01) );  // R7

	PORTE->PCR[28] = ( 0|PORT_PCR_MUX(0x01) ); // #EN

	PORTE->PCR[10] = ( PORT_PCR_ISF(0x01) | PORT_PCR_IRQC(0x0A) | PORT_PCR_MUX(0x01) | PORT_PCR_PE(0x01) | PORT_PCR_PS(0x01));
	PORTE->PCR[11] = ( PORT_PCR_ISF(0x01) | PORT_PCR_IRQC(0x0A) | PORT_PCR_MUX(0x01) | PORT_PCR_PE(0x01) | PORT_PCR_PS(0x01));
	PORTE->PCR[12] = ( PORT_PCR_ISF(0x01) | PORT_PCR_IRQC(0x0A) | PORT_PCR_MUX(0x01) | PORT_PCR_PE(0x01) | PORT_PCR_PS(0x01));
	PORTE->PCR[26] = ( PORT_PCR_ISF(0x01) | PORT_PCR_IRQC(0x0A) | PORT_PCR_MUX(0x01) | PORT_PCR_PE(0x01) | PORT_PCR_PS(0x01));
	PORTE->PCR[27] = ( PORT_PCR_ISF(0x01) | PORT_PCR_IRQC(0x0A) | PORT_PCR_MUX(0x01) | PORT_PCR_PE(0x01) | PORT_PCR_PS(0x01));

	NVIC_ClearPendingIRQ(PORTE_IRQn);
	NVIC_EnableIRQ(PORTE_IRQn);

	SIM->SCGC6 |= SIM_SCGC6_PIT_MASK;

	PIT_MCR = 0x00;
	PIT_TFLG0 |= PIT_TFLG_TIF_MASK;
	PIT_TCTRL0 = PIT_TCTRL_TIE_MASK;
	PIT_TCTRL0 |= PIT_TCTRL_TEN_MASK;
	PIT_LDVAL0 = 100000;

	NVIC_ClearPendingIRQ(PIT0_IRQn);
	NVIC_EnableIRQ(PIT0_IRQn);

	/* Change corresponding PTA port pins as outputs */
	PTA->PDDR = GPIO_PDDR_PDD(0x3F000FC0);

	/* Change corresponding PTE port pins as outputs */
	PTE->PDDR = GPIO_PDDR_PDD( GPIO_PIN(28) );


}


/* Variable delay loop */
void delay(int t1, int t2)
{
	int i, j;

	for(i=0; i<t1; i++) {
		for(j=0; j<t2; j++);
	}
}



/* Conversion of requested column number into the 4-to-16 decoder control.  */
void column_select(unsigned int col_num)
{
	unsigned i, result, col_sel[4];

	for (i =0; i<4; i++) {
		result = col_num / 2;	  // Whole-number division of the input number
		col_sel[i] = col_num % 2;
		col_num = result;

		switch(i) {

			// Selection signal A0
		    case 0:
				((col_sel[i]) == 0) ? (PTA->PDOR &= ~GPIO_PDOR_PDO( GPIO_PIN(8))) : (PTA->PDOR |= GPIO_PDOR_PDO( GPIO_PIN(8)));
				break;

			// Selection signal A1
			case 1:
				((col_sel[i]) == 0) ? (PTA->PDOR &= ~GPIO_PDOR_PDO( GPIO_PIN(10))) : (PTA->PDOR |= GPIO_PDOR_PDO( GPIO_PIN(10)));
				break;

			// Selection signal A2
			case 2:
				((col_sel[i]) == 0) ? (PTA->PDOR &= ~GPIO_PDOR_PDO( GPIO_PIN(6))) : (PTA->PDOR |= GPIO_PDOR_PDO( GPIO_PIN(6)));
				break;

			// Selection signal A3
			case 3:
				((col_sel[i]) == 0) ? (PTA->PDOR &= ~GPIO_PDOR_PDO( GPIO_PIN(11))) : (PTA->PDOR |= GPIO_PDOR_PDO( GPIO_PIN(11)));
				break;

			// Otherwise nothing to do...
			default:
				break;
		}
	}
}

//return mask for each row
int row_select(int row_num){
	switch(row_num){
		case 0:
			return 0b000100000000000000000000000000;
			break;
		case 1:
			return 0b000001000000000000000000000000;
			break;
		case 2:
			return 0b000000000000000000001000000000;
			break;
		case 3:
			return 0b000010000000000000000000000000;
			break;
		case 4:
			return 0b010000000000000000000000000000;
			break;
		case 5:
			return 0b000000000000000000000010000000;
			break;
		case 6:
			return 0b001000000000000000000000000000;
			break;
		case 7:
			return 0b100000000000000000000000000000;
			break;
	}
}

void flash_body(){
	for(int i = 0; i < SNAKE_LEN; i++){
		PTA->PDOR &= GPIO_PDOR_PDO(CLEAR_ROWS);
		column_select(snake[i].y);
		PTA->PDOR |= GPIO_PDOR_PDO(row_select(snake[i].x));
		delay(30,120);
	}
}

int moves(){
	for(int i = SNAKE_LEN - 1; i >= 0; i--){
		if(i > 0){
			snake[i].y = snake[i - 1].y;
			snake[i].x = snake[i - 1].x;
			snake[i].direction = snake[i - 1].direction;
		}
		else{
			switch(snake[i].direction){
				case 1: //UP
					snake[i].y -= 1;
					break;
				case 2: //DOWN
					snake[i].y += 1;
					break;
				case 3: //LEFT
					snake[i].x += 1;
					break;
				case 4: //RIGHT
					snake[i].x -= 1;
					break;
			}
			//edges
			if(snake[i].x > 7)
				snake[i].x = 0;

			if(snake[i].x < 0)
				snake[i].x = 7;

		}
	}


	return 0;

}

void PORTE_IRQHandler(void) {

	if (PORTE->ISFR & BUTTON_RIGHT)
	{
		snake[0].direction = 4;
	}
	else if (PORTE->ISFR & BUTTON_LEFT)
	{
		snake[0].direction = 3;
	}
	else if (PORTE->ISFR & BUTTON_UP)
	{
		snake[0].direction = 1;
	}
	else if (PORTE->ISFR & BUTTON_DOWN)
	{
		snake[0].direction = 2;
	}
	PORTE->ISFR = BUTTON_UP | BUTTON_LEFT  | BUTTON_DOWN  | BUTTON_RIGHT ;
}


void PIT0_IRQHandler(void) {

	for(int i=0; i<3; i++) {
			for(int j=0; j<12; j++){
					flash_body();
			}
	}
	moves();

	PTA->PDOR &= GPIO_PDOR_PDO(CLEAR_ROWS);

	PIT_TFLG0 |= PIT_TFLG_TIF_MASK;
}
//.direction hodnoty
// NAHORU = 1
// DOLU = 2
// VLEVO = 3
// VPRAVO = 4

int main(void)
{
	SystemConfig();

	for(int i = 0; i < SNAKE_LEN; i++){
		snake[i].x = 3+i;
		snake[i].y = 13;
		snake[i].direction = 4;
	}
	//zobraz hada
	flash_body();

    while (1);

    /* Never leave main */
    return 0;
}

