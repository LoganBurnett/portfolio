from django.shortcuts import render
from rest_framework import viewsets
from .serializers import BookSerializer
from .models import BookData
from django.db.models import Max, Min

# Create your views here.
class BookViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.all()
    serializer_class = BookSerializer

class TopRatedViewSet(viewsets.ModelViewSet):
    highest_rating = BookData.objects.all().aggregate(Max('rating'))['rating__max']

    queryset = BookData.objects.filter(rating=highest_rating)
    serializer_class = BookSerializer

class LowRatedViewSet(viewsets.ModelViewSet):
    lowest_rating = BookData.objects.all().aggregate(Min('rating'))['rating__min']

    queryset = BookData.objects.filter(rating=lowest_rating)
    serializer_class = BookSerializer

class FictionViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(category='Fiction')
    serializer_class = BookSerializer

class HistoricalFictionViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(category='Historical Fiction')
    serializer_class = BookSerializer

class RomanceViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(genre='Romance')
    serializer_class = BookSerializer

class FantasyViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(genre='Fantasy')
    serializer_class = BookSerializer

class SciFiViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(genre='Science Fiction')
    serializer_class = BookSerializer

class ChildrenViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(genre="Children's")
    serializer_class = BookSerializer

class TragedyViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(genre='Tragedy')
    serializer_class = BookSerializer

class MysteryViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(genre='Mystery')
    serializer_class = BookSerializer

class HorrorViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(genre='Horror')
    serializer_class = BookSerializer

class GothicViewSet(viewsets.ModelViewSet):
    queryset = BookData.objects.filter(genre='Gothic')
    serializer_class = BookSerializer


# TODO Add 10 more API end points