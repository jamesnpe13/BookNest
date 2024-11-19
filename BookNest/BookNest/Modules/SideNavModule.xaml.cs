using BookNest.Services;
using BookNest.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace BookNest.Modules;

public partial class SideNavModule : UserControl
{

    public SideNavModule()
    {
        InitializeComponent();

    }

    // apply logic here to check if user is
    // member or admin, then change button routing

    private void DashboardNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView(PageView.Dashboard);
    }

    private void BooksNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView(PageView.Books);
    }

    private void BagNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView(PageView.Bag);
    }

    private void WatchlistNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView(PageView.Watchlist);
    }

    private void ReturnsNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView(PageView.Returns);
    }

    private void ReservedNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView(PageView.Reserved);
    }

    private void PeopleNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView(PageView.People);
    }

    private void AccountNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView(PageView.Account);
    }

    private void SignOutNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        //SetCurrentView(PageView.SignOut);
        HandleUserSignOut();
    }

    private void SetCurrentView(PageView targetView)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView(targetView);
        }
    }

    private void HandleUserSignOut()
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.HandleUserSignOut();
        }
        
    }
}
