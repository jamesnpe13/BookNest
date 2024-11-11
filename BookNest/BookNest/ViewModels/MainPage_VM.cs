using BookNest.Modules;
using BookNest.Services;
using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32.SafeHandles;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MainPage_VM : ObservableObject
{
    private readonly AppData ad;
    private readonly SessionService ss;

    [ObservableProperty]
    private UserControl currentView;

    [ObservableProperty]
    private string welcomeTextLine1;

    [ObservableProperty]
    private string welcomeTextLine2;

    [ObservableProperty] private Visibility dashboardNavButtonVisibility;
    [ObservableProperty] private Visibility booksNavButtonVisibility;
    [ObservableProperty] private Visibility bagNavButtonVisibility;
    [ObservableProperty] private Visibility watchlistNavButtonVisibility;
    [ObservableProperty] private Visibility returnsNavButtonVisibility;
    [ObservableProperty] private Visibility reservedNavButtonVisibility;
    [ObservableProperty] private Visibility peopleNavButtonVisibility;
    [ObservableProperty] private Visibility accountNavButtonVisibility;
    [ObservableProperty] private Visibility signOutNavButtonVisibility;

    public MainPage_VM(AppData _ad, SessionService _ss)
    {
        ad = _ad;
        ss = _ss;

        ConstructWelcomeMessage();
        NavBarStyleInit();
        SetCurrentView("MemberDashboard");
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

    // navbar style (member or admin)
    public void NavBarStyleInit()
    {
        if (ad.CurrentAccount.AccountType == "Member")
        {
            DashboardNavButtonVisibility = Visibility.Collapsed;
            BooksNavButtonVisibility = Visibility.Visible;
            BagNavButtonVisibility = Visibility.Visible;
            WatchlistNavButtonVisibility = Visibility.Visible;
            ReturnsNavButtonVisibility = Visibility.Collapsed;
            ReservedNavButtonVisibility = Visibility.Collapsed;
            PeopleNavButtonVisibility = Visibility.Collapsed;
            AccountNavButtonVisibility = Visibility.Visible;
            SignOutNavButtonVisibility = Visibility.Visible;
        }
        else if (ad.CurrentAccount.AccountType == "Administrator")
        {
            DashboardNavButtonVisibility = Visibility.Visible;
            BooksNavButtonVisibility = Visibility.Visible;
            BagNavButtonVisibility = Visibility.Collapsed;
            WatchlistNavButtonVisibility = Visibility.Collapsed;
            ReturnsNavButtonVisibility = Visibility.Visible;
            ReservedNavButtonVisibility = Visibility.Visible;
            PeopleNavButtonVisibility = Visibility.Visible;
            AccountNavButtonVisibility = Visibility.Visible;
            SignOutNavButtonVisibility = Visibility.Visible;
        }
    }

    public void HandleUserSignOut()
    {
        ss.HandleUserSignOut();
    }
}
