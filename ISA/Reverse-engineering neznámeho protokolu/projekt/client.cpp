#include<iostream>
#include <string.h>
#include <fstream>
#include <unistd.h>
#include <stdio.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <errno.h>
#include <netdb.h>
#include <arpa/inet.h>


using namespace std;

//basic values for connecting
#define basicport "32323"
#define basicadress "::1"
#define MAXDATASIZE 2048

//error messages
#define Error_0CommandlineArguments "client: expects <command> [<args>] ... on the command line, given 0 arguments\n";
#define Error_0_p_Arguments "client: the \"-p\" option needs 1 argument, but 0 provided\n";
#define Error_0_a_Arguments "client: the \"-a\" option needs 1 argument, but 0 provided\n";
#define Error_adress "client: only one instance of one option from (-p --port) is allowed\n";
#define Error_port "client: only one instance of one option from (-a --adress) is allowed\n";

//work with login-token file
fstream login_token;

//string used for base64 encoding func
static const std::string base64_chars =
             "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
             "abcdefghijklmnopqrstuvwxyz"
             "0123456789+/";

//check if arg is number
bool is_number(const std::string& s);
//check if app was started properly
int parse_arguments(char s[],int argc,char *argv[]);
//check if arg on command position is command
bool is_command(string argument);
//encoding password for register and login commands
std::string encode(char* bytes_to_encode,int in_len);
//check for ipv4 and ipv6
void *get_in_addr(struct sockaddr *sa);
//edit escape sequences in string
std::string escapecheck(std::string argument);
//clear mallocs
void clear();


char buffer[MAXDATASIZE];
//Position where command is expected
int ComPosition;
//Iterator for string
int ArgNumber;
char *port;
char *adress;
//known commands
const string commands[6]= {"register", "login", "list", "send","fetch", "logout"};





int main(int argc, char *argv[]){
   //check for number of argument and switch options
   for (ArgNumber = 1; ArgNumber < argc; ArgNumber++){
       ArgNumber = parse_arguments(argv[ArgNumber],argc,argv);
       	switch(ArgNumber){
	case -1:
		return 10;
	case -2:
		return 16;
	case -3:
		return 12;
	case -4:
		return 8;
	default:
		continue;
       }
    }
    //zero argument given
    if(ComPosition == 0){
        std::cerr << Error_0CommandlineArguments;
        return 10;
    }
     
    if(!(is_command(std::string(argv[ComPosition])))){
        std::cerr << "unknown command\n";
        return 8;
    }
    //set basic values of port and adress
    if(port == NULL){
        port = (char*) malloc(sizeof(char)*strlen(basicport));
        strcpy(port,basicport);
    }
    if(adress == NULL){
        adress = (char*) malloc(sizeof(char)*strlen(basicadress));
        strcpy(adress,basicadress);
    }
    //connecting
    int sockfd;  
    struct addrinfo hints, *servinfo, *p;
    int rv;
    char s[INET6_ADDRSTRLEN];
    //set up values 
    memset(&hints, 0, sizeof hints);
    hints.ai_family = AF_UNSPEC;
    hints.ai_socktype = SOCK_STREAM;
      
    if ((rv = getaddrinfo(adress, port, &hints, &servinfo)) != 0) {
        fprintf(stderr, "getaddrinfo: %s\n", gai_strerror(rv));
        return 24;
    }
    for(p = servinfo; p != NULL; p = p->ai_next) {
        if ((sockfd = socket(p->ai_family, p->ai_socktype, p->ai_protocol)) == -1) {
            perror("client: socket");
            return 24;   
        }

        if (connect(sockfd, p->ai_addr, p->ai_addrlen) == -1) {
            close(sockfd);
            perror("client: connect");
            return 24;
        }
 	if (p == 0) {
            fprintf(stderr, "client: failed to connect");
	    return 24;
        } 
	    string toSend;
	//prepare message for server by arguments
	 inet_ntop(p->ai_family, get_in_addr((struct sockaddr *)p->ai_addr), s,sizeof s);
	 if((std::string(argv[ComPosition]) == "register") || (std::string(argv[ComPosition]) == "login")){
		    char *PasswordPointer;
            	    PasswordPointer = (char *) malloc(strlen(argv[ComPosition + 2]));
            	    strcpy(PasswordPointer,argv[ComPosition + 2]);
            	    string password = encode(PasswordPointer, strlen(argv[ComPosition + 2]));
		    toSend = "("+std::string(argv[ComPosition])+" \""+std::string(argv[ComPosition+1])+"\" "+"\""+password+"\" "+")"+"\n";
	  }
	  else if((std::string(argv[ComPosition]) == "list") || (std::string(argv[ComPosition]) == "logout")){
		     ifstream login_token("login-token");
 		     getline(login_token,toSend);
		     if(toSend.empty()){
			fprintf(stderr, "Not logged in\n");
			return 0;
		     } 
	    	     toSend = "("+std::string(argv[ComPosition])+" "+toSend+")"+"\n";

	    }
	    else if(std::string(argv[ComPosition]) == "fetch"){
		ifstream login_token("login-token");
            	getline(login_token,toSend);
		if(toSend.empty()){
			fprintf(stderr, "Not logged in\n");
			return 0;
		} 
 	        toSend = "("+std::string(argv[ComPosition])+" "+toSend+" "+std::string(argv[ComPosition+1])+")"+"\n";
	    }
	    else if(std::string(argv[ComPosition]) == "send"){
		ifstream login_token("login-token");
            	getline(login_token,toSend);
		if(toSend.empty()){
			fprintf(stderr, "Not logged in\n");
			return 0;
		}
 	        toSend = "("+std::string(argv[ComPosition])+" "+toSend+" "+"\""+escapecheck(std::string(argv[ComPosition+1]))+"\""+" "+"\""+escapecheck(std::string(argv[ComPosition+2]))+"\""+" "+"\""+escapecheck(std::string(argv[ComPosition+3]))+"\""+")"+"\n";
	   }
	    send(sockfd,toSend.c_str(),toSend.length(),0);
	    int count = 0;
	    int total = 0;
	    //loop for recieving data
	    while ((count = recv(sockfd, &buffer[total], sizeof buffer - total, 0)) > 0)
	    {
		total += count;
	    }
	    if (count == -1)
	    {
		perror("recv");
		return(30);
	    }
	}
    //change answer from server to client output
    string answ = std::string(buffer);
    if(answ.substr(0,3) == "(ok"){
        if(std::string(argv[ComPosition]) == "fetch"){
	
            cout << "SUCCES:\n\n";
			int position = 6;
			int temp = 0;
			temp = position;
			//iteration through the string(prevent escape sequences)
			while(1){
				 if(answ[position] == '\\'){
					answ.erase(answ.begin()+position);
					position ++;
				}
				else if(answ[position] == '\"') break;
					position++;
			}
			cout << "From: "<< answ.substr(temp,position-temp) << endl;
			position += 3;
			temp = position;
			while(1){
	 			if(answ[position] == '\\'){
					answ.erase(answ.begin()+position);
					position ++;
				}
				else if(answ[position] == '\"') break;
					position++;
			}
			cout << "Subject: "<< answ.substr(temp,position-temp) << endl;
			position += 3;
			temp = position;
			while(1){
	 			if(answ[position] == '\\'){
					answ.erase(answ.begin()+position);
					position ++;
				}
				else if(answ[position] == '\"') break;
					position++;
			}
			cout <<endl << answ.substr(temp,position-temp);
        }
        else if(std::string(argv[ComPosition]) == "list"){
	       	cout << "SUCCES:\n";
           	if(!(answ.length() == 7)){
			unsigned int position = 5;
			unsigned int temp = 0;
			string number;
			//loop for every returned item (contains loops like in list)
			while (position < answ.length() - 3){
	 			if(isdigit(answ[position+1])){
					number = answ[position +1];
					position++;
					if(isdigit(answ[position+2])){
						number = answ.substr(position, position +2);
						position ++;
					}
					cout << number << ":" << endl;	
				}
				else break;	
				position += 3;
				temp = position;
				while(1){
               				if(answ[position] == '\\'){
 						 position ++;
					}
					if(answ[position] == '\"') break;
 				position++;
				}
				cout << "  " << "From: "<< answ.substr(temp,position-temp) << endl;
				position += 3;
				temp = position;
				while(1){
				 	if(answ[position] == '\\'){
						answ.erase(answ.begin()+position);
						position ++;
					}
					else if(answ[position] == '\"') break;
				position++;
				}
				cout << "  " << "Subject: "<< answ.substr(temp,position-temp) << endl;
				position += 3;
			 }
		}
        }
            
        else{
            cout << "SUCCESS: " << answ.substr(answ.find("\"")+1,(answ.substr((answ.find("\"")+1)).find("\"")))<< endl;
            if(answ.substr(5,14) == "user logged in"){
                login_token.open("login-token", ios::out);
                int position = answ.find_last_of("\"");
                login_token << answ.substr((answ.substr(0,position)).find_last_of("\""),position-20);
            }
	    }
	    if(answ.substr(5,10) == "logged out"){
		    remove("login-token");
	    }
    }
    if(answ.substr(0,3) == "(er"){
	cout << "ERROR: "<< answ.substr(answ.find("\"")+1,(answ.substr((answ.find("\"")+1)).find("\""))) << endl;
    }
    //clear trash
    close(sockfd);
    freeaddrinfo(servinfo);
    clear();

    return 0;
}
//check if string is number
bool is_number(const std::string& s){
    std::string::const_iterator it = s.begin();
    while (it != s.end() && std::isdigit(*it)) ++it;
    return !s.empty() && it == s.end();
}
//check terminal input
int parse_arguments(char s[],int argc,char *argv[])
{
    if(strcmp(s,"logout") == 0){
        if(ArgNumber+1 != argc){
            std::cerr << "logout\n";
            return -1;
        }
        else{
            ComPosition = ArgNumber;
            return ArgNumber++;
        }
    }
    else if(strcmp(s,"fetch") == 0){
        if(ArgNumber+2 != argc){
            std::cerr << "fetch <id>\n";
            return -1;
        }
        else{
            if(is_number(argv[ArgNumber+1])){
                ComPosition = ArgNumber;
                return ArgNumber+2;
            }
            else{ 
		std::cerr << "ERROR: id " << argv[ArgNumber+1] << " is not a number";
		return -2;
	    }        
	}
    }
    else if(strcmp(s,"send") == 0){ 
        if(ArgNumber+4 != argc){
            std::cerr << "send <recipient> <subject> <body>\n";
            return -1;
        }
        else{
            ComPosition = ArgNumber;
            return ArgNumber+4;

        }

    }
    else if(strcmp(s,"list") == 0){
        if(ArgNumber+1 != argc){
            std::cerr << "list\n";
            return -1;
        }
        else{
            ComPosition = ArgNumber;
            return ArgNumber++;
        }
    }
    else if(strcmp(s,"login") == 0){
        if(ArgNumber+3 != argc){
            std::cerr << "login <username> <password>\n";
            return -1;
        }
        else{
            ComPosition = ArgNumber;
            return ArgNumber+3;
        }
    }
    else if(strcmp(s,"register") == 0){ //todo
        if(ArgNumber+3 != argc){
            std::cerr << "register <username> <password>\n";
            return -1;
        }
        else{
            ComPosition = ArgNumber;
            return ArgNumber+3;
        }
    }
    else if((strcmp(s,"-p") == 0) || strcmp(s,"-port") == 0){
        if( port != NULL){
            std::cerr << Error_port;
            return -3;
        }
        if(ArgNumber+1 == argc){

            std::cerr << Error_0_p_Arguments;
            return -1;
        }
        else if (is_number(argv[ArgNumber+1])){
            port = (char*) malloc(sizeof(char)*strlen(argv[ArgNumber+1]));
            strcpy(port,argv[ArgNumber+1]);
            if( ArgNumber+1 == argc){
                std::cerr << Error_0CommandlineArguments;
                return -1;
            } 
            else return ArgNumber+1;
        }
        else {
            std::cerr << "Port number is not a string\n";
            return -2;
        }
    }
    else if((strcmp(s,"-a") == 0) || strcmp(s,"-adress") == 0){
        if( adress != NULL){
            std::cerr << Error_adress;
            return -3;
        }
        if(ArgNumber+1 == argc){
            std::cerr << Error_0_a_Arguments;
            return -1;
        }
        else{
            if((ArgNumber+2) ==argc){
                std::cerr << Error_0CommandlineArguments;
                return -1;
            }
            else{
                adress = (char*) malloc(sizeof(char)*strlen(argv[ArgNumber+1]));
                strcpy(adress,argv[ArgNumber+1]);
                return ArgNumber+1;
            }
        }
    }
    // help output
    else if(strcmp(s,"-h") == 0 || strcmp(s,"-help")  == 0 || strcmp(s,"--help")  == 0){
        std::cout << "\nusage: client [ <option> ... ] <command> [<args>] ...\n\n";
        std::cout << "<option> is one of\n\n";
        std::cout << "-a <addr>, --address <addr>\n";
        std::cout << "\t\tServer hostname or address to connect to\n";
        std::cout << "\t-p <port>, --port <port>\n";
        std::cout << "\t\tServer port to connect to\n";
        std::cout << "\t--help, -h\n";
        std::cout << "\t\tShow this help\n";
        std::cout << "\t--\n";
        std::cout << "\t\tDo not treat any remaining argument as a switch (at this level)\n\n";
        std::cout << "\tMultiple single-letter switches can be combined after\n";
        std::cout << "\tone `-`. For example, `-h-` is the same as `-h --`.\n";
        std::cout << "\tSupported commands:\n";
        std::cout << "\t\tregister <username> <password>\n";
        std::cout << "\t\tlogin <username> <password>\n";
        std::cout << "\t\tlist\n";
        std::cout << "\t\tsend <recipient> <subject> <body>\n";
        std::cout << "\t\tfetch <id>\n";
        std::cout << "\t\tlogout\n";
        return 0;
    }
    else if(strcmp(s,"--") == 0){
        ComPosition = ArgNumber + 1;
        return ArgNumber++;
    }
    //check for unknown switches
    else if(std::string(s).find("-")== 0){
            std::cerr << "client: unknown switch: " << s<<"\n";
            return -4;
    }
    else{
        cerr <<"unknown command\n";
        return -1;
    }
    return ArgNumber;
}
//clering mallocs
void clear(){
    free(port);
    free(adress);
}
//check for escape sequences to replace them with one additional backslash
std::string escapecheck(std::string argument){
	unsigned int pos = 0;
	while(pos != argument.length()){
		if(argument[pos]== '\"'){
			argument.replace(pos,1,"\\\"");
			pos++;
		}
		else if(argument[pos]== '\\'){
			argument.replace(pos,1,"\\\\");
			pos++;
		}
		pos++;
	}
	return argument;
}
//check if argument on command postitin in terminal is command
//commands = list of known commands
bool is_command(string string){
    for (int i = 0; i < 6; i++){
        if(commands[i] == string) return true;
    }
    return false;
}

//encoding password to base64 for commands register and login 
std::string encode(char* bytes_to_encode, int in_len){
  std::string ret;
  int i = 0;
  int j = 0;
  unsigned char char_array_3[3];
  unsigned char char_array_4[4];

  while (in_len--) {
    char_array_3[i++] = *(bytes_to_encode++);
    if (i == 3) {
      char_array_4[0] = (char_array_3[0] & 0xfc) >> 2;
      char_array_4[1] = ((char_array_3[0] & 0x03) << 4) + ((char_array_3[1] & 0xf0) >> 4);
      char_array_4[2] = ((char_array_3[1] & 0x0f) << 2) + ((char_array_3[2] & 0xc0) >> 6);
      char_array_4[3] = char_array_3[2] & 0x3f;

      for(i = 0; (i <4) ; i++)
        ret += base64_chars[char_array_4[i]];
      i = 0;
    }
  }

  if (i)
  {
    for(j = i; j < 3; j++)
      char_array_3[j] = '\0';

    char_array_4[0] = (char_array_3[0] & 0xfc) >> 2;
    char_array_4[1] = ((char_array_3[0] & 0x03) << 4) + ((char_array_3[1] & 0xf0) >> 4);
    char_array_4[2] = ((char_array_3[1] & 0x0f) << 2) + ((char_array_3[2] & 0xc0) >> 6);
    char_array_4[3] = char_array_3[2] & 0x3f;

    for (j = 0; (j < i + 1); j++)
      ret += base64_chars[char_array_4[j]];

    while((i++ < 3))
      ret += '=';

  }

  return ret;

}

void *get_in_addr(struct sockaddr *sa){
	if(sa->sa_family == AF_INET){
		return &(((struct sockaddr_in*)sa)->sin_addr);
	}
	return &(((struct sockaddr_in6*)sa)->sin6_addr);
}

