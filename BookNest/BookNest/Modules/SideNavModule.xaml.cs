using BookNest.Services;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Modules;

public partial class SideNavModule : UserControl
{
    public SideNavModule() => InitializeComponent();

    // method for sending message to navigate between pages
    public void NavigateToPage(string page) => WeakReferenceMessenger.Default.Send(new NavigateToPage_Message(page));

    // navigate to page on click
    private void SignOutButton_MouseDown(object sender, MouseButtonEventArgs e) => NavigateToPage("SignInPage");
}
