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
        SetCurrentView("MemberDashboard");
    }

    private void BooksNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView("MemberBooks");
    }

    private void BagNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView("MemberBag");
    }

    private void WatchlistNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView("MemberWatchlist");
    }

    private void ReturnsNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView("AdminReturns");
    }

    private void ReservedNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView("AdminReserved");
    }

    private void PeopleNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView("AdminPeople");
    }

    private void AccountNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SetCurrentView("MemberAccount");
    }

    private void SignOutNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        //SetCurrentView("SignOut");
        HandleUserSignOut();
    }

    private void SetCurrentView(string targetView)
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
