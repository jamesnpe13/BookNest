using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace BookNest.ViewModels;

public partial class SignInPage_VM : ObservableObject
{
    private readonly PageNavigationService ps;

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string password;

    public SignInPage_VM(PageNavigationService _ps)
    {
        ps = _ps;
    }

    public void SubmitForm()
    {
        // input validation
        ps.SetCurrentPage("MainPage"); // bypass validation
    }
}
