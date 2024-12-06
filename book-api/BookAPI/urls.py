"""
URL configuration for BookAPI project.

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/5.1/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path, include
from rest_framework import routers
from books.views import (
    BookViewSet, TopRatedViewSet, FictionViewSet, HistoricalFictionViewSet, RomanceViewSet, LowRatedViewSet, FantasyViewSet, SciFiViewSet,
    ChildrenViewSet, TragedyViewSet, MysteryViewSet, HorrorViewSet, GothicViewSet
)
from django.conf.urls.static import static
from django.conf import settings

router = routers.SimpleRouter()
router.register('books', BookViewSet)
router.register('top-rated', TopRatedViewSet, basename="top-rated")
router.register('lowest-rated', LowRatedViewSet, basename="lowest-rated")
router.register('fiction', FictionViewSet, basename="fiction")
router.register('historical-fiction', HistoricalFictionViewSet, basename="historical-fiction")
router.register('romance', RomanceViewSet, basename="romance")
router.register('fantasy', FantasyViewSet, basename="fantasy")
router.register('sci-fi', SciFiViewSet, basename="sci-fi")
router.register('children', ChildrenViewSet, basename="children")
router.register('tragedy', TragedyViewSet, basename="tragedy")
router.register('mystery', MysteryViewSet, basename="mystery")
router.register('horror', HorrorViewSet, basename="horror")
router.register('gothic', GothicViewSet, basename="gothic")

urlpatterns = [
    path('', include(router.urls)),
    path('admin/', admin.site.urls),
]+static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
