from django.contrib.auth.decorators import user_passes_test
from django.shortcuts import render

# Create your views here.
from django.http import HttpResponse
import datetime


# Not a Perfekcyjna Niedoskonałość reference
@user_passes_test(lambda u: u.is_superuser)
def plateau(request):
    now = datetime.datetime.now()
    html = "<html><body>It is now %s.</body></html>" % now
    return render(request, 'plateau.html')
