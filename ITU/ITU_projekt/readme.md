## How to run on Windows 10
Requires Python 3.8, make sure pip is included in the installation

Unzip ITU_projekt.zip, navigate to the folder containing the project folder,<br>
Open the Command Prompt (not PowerShell) and enter:

`py -m venv ITU_projekt`, then<br>
`ITU_projekt\Scripts\activate.bat`

Then navigate to the project folder `cd ITU_projekt`, and enter:

`pip install -r requirements.txt`

Then start the server with:

`python manage.py runserver`

Enter the displayed link into the web browser.

In case you are running into errors displaying the site you may need to try:

`python manage.py makemigrations`<br>
`python manage.py migrate --run-syncdb`


