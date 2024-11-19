using BookNest.Services;
using BookNest.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace BookNest.Modules;

public partial class SideNavModule : UserControl
{
    private readonly SessionService ss;
    private readonly MainPage_VM vm;

    public SideNavModule()
    {
        InitializeComponent();
        ss = ((App)Application.Current).ServiceProvider.GetRequiredService<SessionService>();
        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();

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
        vm.SetCurrentView(targetView);
    }

    private void HandleUserSignOut()
    {
        ss.HandleUserSignOut();
    }
}
