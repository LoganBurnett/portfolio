from django.db import models
from django.contrib.auth.models import User
from django.db.models.signals import post_delete
from django.dispatch import receiver

# Create your models here.
class Post(models.Model):
    user = models.ForeignKey(User, related_name="posts", on_delete=models.CASCADE)
    date = models.DateTimeField(auto_now_add=True)  # Stored in UTC
    text = models.TextField(max_length=300)
    image = models.ImageField(null=True, blank=True, upload_to='user_images')

    def __str__(self):
        return self.user.username + ": " + str(self.date)

# Deletes the post image when post is deleted
@receiver(post_delete, sender=Post)
def delete_image(sender, instance, **kwargs):
    if instance.image:
        instance.image.delete(save=False)

class Comment(models.Model):
    post = models.ForeignKey(Post, related_name="comments", on_delete=models.CASCADE)
    user = models.ForeignKey(User, related_name="comments", on_delete=models.CASCADE)
    date = models.DateTimeField(auto_now_add=True)
    content = models.TextField(max_length=300)

    class Meta:
        ordering = ['-date']

    def __str__(self):
        return self.user.username + ": " + str(self.date)
    
class Like(models.Model):
    post = models.ForeignKey(Post, related_name="likes", on_delete=models.CASCADE)
    user = models.ForeignKey(User, on_delete=models.CASCADE)

    class Meta:
        constraints = [
            models.UniqueConstraint(fields=['post', 'user'], name='unique_post_user_like')
        ]

class Follow(models.Model):
    # The user that is now following another user
    follower = models.ForeignKey(User, related_name="followings", on_delete=models.CASCADE)
    # The user that is being followed by the follower
    following = models.ForeignKey(User, related_name="followers", on_delete=models.CASCADE)

    class Meta:
        constraints = [
            models.UniqueConstraint(fields=['follower', 'following'], name='unique_follow')
        ]