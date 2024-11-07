using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace BookNest.ViewModels;

partial class SignInPage_VM : ObservableObject
{
    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string password;

    public void SubmitForm()
    {
        // input validation
    }
}
