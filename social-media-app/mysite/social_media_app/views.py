from django.shortcuts import render, redirect
from django.core.paginator import Paginator
from django.db.models import Q
from .models import Post, Follow, Like
from django.contrib.auth.decorators import login_required
from django.contrib.auth.models import User
from .util import postFormHelper

# Create your views here.
def index(request):
    user = request.user
    postFormHelper(request, user)

    posts = Post.objects.all().order_by("-date")
    
    search_term = request.GET.get("search_term")
    if search_term != '' and search_term is not None:
        posts = posts.filter(
            Q(user__profile__display_name__icontains=search_term) | 
            Q(text__icontains=search_term)
        )

    paginator = Paginator(posts, 5)
    page = request.GET.get('page')

    posts = paginator.get_page(page)

    if user.is_authenticated:
        liked_posts = Like.objects.filter(user=user).values_list('post_id', flat=True)
        return render(request, 'social_media/index.html', {'posts': posts, 'liked_posts': liked_posts, 'search_term': search_term})
    else:
        return render(request, 'social_media/index.html', {'posts': posts, 'liked_posts': [], 'search_term': search_term})

@login_required
def new_post(request):
    if request.method == 'POST':
        user = request.user
        text = request.POST.get('text', '')
        image = request.FILES.get('image')

        post = Post(user=user, text=text, image=image)
        post.save()

        return redirect('index')

    return render(request, 'social_media/new_post.html')

def sm_profile(request, id):
    if request.user.is_authenticated and id == request.user.id:
        return redirect('profile')
    
    postFormHelper(request, request.user)

    pf_user = User.objects.get(pk=id)
    posts = Post.objects.filter(user=pf_user).order_by('-date')
    
    if request.user.is_authenticated:
        liked_posts = Like.objects.filter(user=request.user).values_list('post_id', flat=True)
        is_following = Follow.objects.filter(follower=request.user, following=pf_user).exists()
        return render(request, 'social_media/profile.html', {'pf_user': pf_user, 'is_following': is_following, 'posts': posts, 'liked_posts': liked_posts})
    else:
        return render(request, 'social_media/profile.html', {'pf_user': pf_user, 'is_following': False, 'posts': posts, 'liked_posts': []})

@login_required
def feed(request):
    user = request.user
    postFormHelper(request, user)

    followings = Follow.objects.filter(follower=user).values_list('following_id', flat=True)
    followings = list(followings)
    followings.append(user.id)

    posts = Post.objects.filter(user_id__in=followings).order_by("-date")
    liked_posts = Like.objects.filter(user=user).values_list('post_id', flat=True)

    search_term = request.GET.get("search_term")
    if search_term != '' and search_term is not None:
        posts = posts.filter(
            Q(user__profile__display_name__icontains=search_term) | 
            Q(text__icontains=search_term)
        )

    paginator = Paginator(posts, 5)
    page = request.GET.get('page')

    posts = paginator.get_page(page)

    return render(request, 'social_media/index.html', {'posts': posts, 'liked_posts': liked_posts, 'search_term': search_term, 'title': "My Feed"})