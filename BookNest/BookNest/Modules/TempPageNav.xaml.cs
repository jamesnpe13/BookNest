using BookNest.Services;
using BookNest.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Modules;

public partial class TempPageNav : UserControl
{

    public TempPageNav()
    {
        InitializeComponent();
    }

    // page navigation router
    private void SignInPageButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        NavigateToPage("SignInPage");
    }

    private void RegistrationPageButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        NavigateToPage("RegistrationPage");
    }

    private void MainPageButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        NavigateToPage("MainPage");
    }

    private void NavigateToPage(string targetPage)
    {
        if (DataContext is MainWindow_VM vm)
            vm.NavigateToPage(targetPage);
    }
}
