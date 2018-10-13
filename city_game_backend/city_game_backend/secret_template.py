# This is how you should configure the secret.py file inside the city_game_backend directory (same as this file)
# The secret.py file stores the secret configuration that cannot be shared inside VCS for security reasons

APP_SECRET = 'write some random letters here'

DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.mysql',
        'NAME': 'DATABASE NAME',
        'USER': 'YOUR DATABASE USER NAME',
        'PASSWORD': 'USER\'S PASSWORD',
        'HOST': 'localhost',   # Or an IP Address that your DB is hosted on
        'PORT': '3306',
    }
}

# Set this to false in production
DEBUG = True
