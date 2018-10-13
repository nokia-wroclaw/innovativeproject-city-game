# Backend Server

### How to setup
**Make sure you are inside the same directory as the `manage.py` file first!** 

* Create a Python virtual environment and activate it:

`virtualenv venv && . venv/bin/activate`

* Install the requirements

`pip install -r requirements.txt`

* Configure the `secret.py` file - refer to the `secret_template.py` inside the `city_game_backend` folder

* Test if the server will run

`python manage.py runserver`

* Stop the server, make the database migrations

`python manage.py migrate`

* Create a superuser account

`python manage.py createsuperuser`

* Start the server again (`python manage.py runserver`) and try to log in as the superuser:

`http://localhost:8000/admin/`