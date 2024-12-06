from django.shortcuts import render, redirect
from django.contrib import messages
from django.contrib.auth import authenticate, login
from django.contrib.auth.decorators import login_required
from .forms import RegistrationForm, ProfileEditForm
from social_media_app.models import Post, Like
from .models import Profile
from social_media_app.util import postFormHelper

# Create your views here.
def register(request):
    if request.method == 'POST':
        form = RegistrationForm(request.POST)
        if form.is_valid():
            new_user = form.save()

            username = form.cleaned_data.get('username')
            messages.success(request, f'Welcome {username}! You are now logged in!')

            new_user = authenticate(username=form.cleaned_data['username'], password=form.cleaned_data['password1'],)
            login(request, new_user)

            return redirect('index')
    else:
        form = RegistrationForm()

    return render(request, 'users/register.html', {'form': form})

@login_required
def profile(request):
    postFormHelper(request, request.user)

    posts = Post.objects.filter(user=request.user).order_by('-date')
    liked_posts = Like.objects.filter(user=request.user).values_list('post_id', flat=True)

    return render(request, 'users/profile.html', {'posts': posts, 'profile': True, 'liked_posts': liked_posts})

@login_required
def edit_profile(request):
    profile = Profile.objects.get(user=request.user)

    if request.method == 'POST':
        form = ProfileEditForm(request.POST, instance=profile)
        if form.is_valid():
            form.save()
            messages.success(request, 'Profile updated!')
            return redirect('profile')
    else:
        form = ProfileEditForm(instance=profile)
    
    return render(request, 'users/edit_profile.html', {'form': form})