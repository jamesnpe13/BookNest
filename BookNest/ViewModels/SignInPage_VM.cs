using BookNest.Components;
using BookNest.Data;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.CodeDom;
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
    private string username = string.Empty;

    [ObservableProperty]
    private string formLabelText = string.Empty;

    [ObservableProperty]
    private string switchTypeButtonText = string.Empty;

    public SignInPage_VM(PageNavigationService _ps, SessionService _ss, AppData _ad, DatabaseService _ds)
    {
        ps = _ps;
        ss = _ss;
        ad = _ad;
        ds = _ds;
        Console.WriteLine("di loaded");
        SetFormType();
    }

    public void SetFormType()
    {
        // display appropriate form label amd switch type button
        FormLabelText = IsAdmin ? "Administrator Sign In" : "Member Sign In";
        SwitchTypeButtonText = IsAdmin ? "Sign in as member" : "Sign in as administrator";
    }

    public void SubmitForm(string password)
    {
        try
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("Please enter a username.");
            if (string.IsNullOrEmpty(password)) throw new Exception("Please enter a password.");

            ss.HandleUserSignIn(Username, password, IsAdmin ? "Administrator" : "Member");
        }
        catch (Exception err)
        {
            NotificationService.Instance.AddNotificationItem(NotificationToastStyle.Error, err.Message);
        }

    }

}
