using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Runtime.CompilerServices;

namespace BookNest.ViewModels;

public partial class SignInPage_VM : ObservableObject
{
    private readonly PageNavigationService ps;
    private readonly SessionService ss;
    private readonly AppData ad;

    [ObservableProperty]
    private bool isAdmin = true; // temporary override

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string formLabelText;

    [ObservableProperty]
    private string switchTypeButtonText;

    public SignInPage_VM(PageNavigationService _ps, SessionService _ss, AppData _ad)
    {
        ps = _ps;
        ss = _ss;
        ad = _ad;
        SetFormType();
    }

    public void SetFormType()
    {
        // display appropriate form label amd switch type button
        FormLabelText = IsAdmin ? "Administrator Sign In" : "Member Sign In";
        SwitchTypeButtonText = IsAdmin ? "Sign in as member" : "Sign in as administrator";
    }

    public void SubmitForm()
    {
        ad.CurrentAccount = ss.CreateTempAccount("TemporaryName");

        //ss.HandleSignIn(Username, Password) {

        //};

        // input validation
        ps.SetCurrentPage("MainPage"); // bypass validation
    }

}
