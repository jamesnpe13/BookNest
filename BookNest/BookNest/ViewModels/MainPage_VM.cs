using BookNest.Services;
using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MainPage_VM : ObservableObject
{
    private readonly AppData ad;

    [ObservableProperty]
    private UserControl currentView;

    [ObservableProperty]
    private string welcomeTextLine1;

    [ObservableProperty]
    private string welcomeTextLine2;

    public MainPage_VM(AppData _ad)
    {
        ad = _ad;
        // default 
        SetCurrentView("MemberDashboard");
        ConstructWelcomeMessage();
    }

    // view router
    [RelayCommand]
    public void SetCurrentView(string targetView)
    {
        if (targetView == "MemberDashboard") CurrentView = new Member_Dashboard_V();
        if (targetView == "MemberBag") CurrentView = new Member_Bag_V();
        if (targetView == "MemberBooks") CurrentView = new Member_Books_V();
        if (targetView == "MemberWatchlist") CurrentView = new Member_Watchlist_V();
        if (targetView == "MemberAccount") CurrentView = new Member_Account_V();
    }

    public void ConstructWelcomeMessage()
    {
        WelcomeTextLine1 = "Hi, " + ad.CurrentAccount.FirstName;
        WelcomeTextLine2 = "Let's get started";
    }
}
