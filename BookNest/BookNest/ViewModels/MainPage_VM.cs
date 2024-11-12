using BookNest.Services;
using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MainPage_VM : ObservableObject
{
    private readonly AppData ad;
    private readonly SessionService ss;

    [ObservableProperty]
    private UserControl? currentView;

    [ObservableProperty]
    private string welcomeTextLine1 = string.Empty;

    [ObservableProperty]
    private string welcomeTextLine2 = string.Empty;

    [ObservableProperty]
    private string accountType = string.Empty;

    [ObservableProperty] private Visibility dashboardNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility booksNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility bagNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility watchlistNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility returnsNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility reservedNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility peopleNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility accountNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility signOutNavButtonVisibility = Visibility.Collapsed;

    public MainPage_VM(AppData _ad, SessionService _ss)
    {
        ad = _ad;
        ss = _ss;

        NavbarInit();
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

    // navbar style (member or admin)
    public void NavbarInit()
    {
        // set welcome message
        WelcomeTextLine1 = "Hi, " + ad.CurrentAccount.FirstName ?? "User";
        WelcomeTextLine2 = "Let's get started";

        // set account type display
        AccountType = ad.CurrentAccount.AccountType;

        // show view buttons
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
        if (ad.CurrentAccount.AccountType == "Administrator")
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

    public void HandleUserSignOut() => ss.HandleUserSignOut();

}
