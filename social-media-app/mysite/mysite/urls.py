"""
URL configuration for mysite project.

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
from django.urls import path
from social_media_app import views
from users import views as userViews
from django.contrib.auth import views as authenticationViews
from django.conf import settings
from django.conf.urls.static import static

urlpatterns = [
    path('admin/', admin.site.urls),
    path('', views.index, name="index"),
    path('posts/new/', views.new_post, name="new_post"),
    path('profile/<int:id>', views.sm_profile, name="sm_profile"),
    path('feed/', views.feed, name="feed"),
    path('register/', userViews.register, name="register"),
    path('login/', authenticationViews.LoginView.as_view(template_name='users/login.html'), name="login"),
    path('logout/', authenticationViews.LogoutView.as_view(template_name='users/logout.html'), name="logout"),
    path('profile/', userViews.profile, name="profile"),
    path('profile/edit/', userViews.edit_profile, name="edit_profile"),
] + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
