using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace BookNest.ViewModels;

partial class MainPage_VM : ObservableObject
{
    [ObservableProperty]
    private UserControl currentView;

    public MainPage_VM()
    {
        // default 
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
}
