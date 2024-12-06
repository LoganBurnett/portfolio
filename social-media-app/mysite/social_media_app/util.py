from .models import Post, Comment, Like, Follow
from django.contrib.auth.models import User

def postFormHelper(request, user):
    if request.method == 'POST':
        form_type = request.POST.get('form_type')
        if form_type == 'comment':
            post_id = int(request.POST.get('post_id'))
            post = Post.objects.get(pk=post_id)
            content = request.POST.get('content')

            comment = Comment(user=user, post=post, content=content)
            comment.save()
        
        if form_type == 'like':
            post_id = int(request.POST.get('post_id'))
            post = Post.objects.get(pk=post_id)

            existing_like = Like.objects.filter(user=user, post=post)
            if existing_like.exists():
                existing_like.delete()
            else:
                like = Like(post=post, user=user)
                like.save()
        
        if form_type == 'follow':
            pf_user_id = int(request.POST.get('user_id'))
            pf_user = User.objects.get(pk=pf_user_id)

            existing_follow = Follow.objects.filter(follower=user, following=pf_user)
            if existing_follow.exists():
                existing_follow.delete()
            else:
                follow = Follow(follower=user, following=pf_user)
                follow.save()