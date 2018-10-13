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