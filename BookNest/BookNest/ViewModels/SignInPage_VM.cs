using BookNest.Data;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace BookNest.ViewModels;

public partial class SignInPage_VM : ObservableObject
{
    private readonly PageNavigationService ps;
    private readonly SessionService ss;
    private readonly AppData ad;
    private readonly DatabaseService ds;

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

    public SignInPage_VM(PageNavigationService _ps, SessionService _ss, AppData _ad, DatabaseService _ds)
    {
        ps = _ps;
        ss = _ss;
        ad = _ad;
        ds = _ds;
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
        // if form validation success then proceed. Else show error and do not proceed.

        // handle sign in
        if (ds.GetAccount_single(Username, IsAdmin ? "Administrator" : "Member").Username == Username)
        {
            Console.WriteLine("Match found");
        }
        else
        {
            Console.WriteLine("Match not found");

        }

    }

}
