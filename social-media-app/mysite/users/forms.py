from django import forms
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm
from .models import Profile

class RegistrationForm(UserCreationForm):
    email = forms.EmailField(required=True)
    display_name = forms.CharField(
        max_length=100, 
        required=True,
        help_text="Enter a display name that others will see. This can be changed later."
    )

    class Meta:
        model = User
        fields = ['email', 'username', 'password1', 'password2']

    def save(self, commit=True):
        user = super().save(commit=commit)

        Profile.objects.create(
            user=user,
            display_name=self.cleaned_data['display_name']
        )

        return user
    
class ProfileEditForm(forms.ModelForm):
    class Meta:
        model = Profile
        fields = ['display_name']