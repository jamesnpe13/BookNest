using BookNest.Services;
using BookNest.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using System.Windows.Input;

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
        // check user type and add logic for routing
        if (DataContext is MainPage_VM vm)
        {           
            vm.SetCurrentView("MemberDashboard");
        }
    }

    private void BooksNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        // check user type and add logic for routing
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView("MemberBooks");
        }
    }

    private void BagNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView("MemberBag");
        }
    }

    private void WatchlistNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView("MemberWatchlist");
        }
    }

    private void ReturnsNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView("AdminReturns");
        }
    }

    private void ReservedNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView("AdminReserved");
        }
    }

    private void PeopleNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView("AdminPeople");
        }
    }

    private void AccountNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        // check user type and add logic for routing
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView("MemberAccount");
        }
    }

    private void SignOutNavButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView("SignOut");
        }
    }
}
