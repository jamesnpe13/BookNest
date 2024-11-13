using BookNest.ViewModels;
using System.Windows.Controls;

namespace BookNest.Views;

public partial class Admin_Dashboard_V : UserControl
{
    public Admin_Dashboard_V()
    {
        InitializeComponent();
    }

    private void ManageBooksButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => SetCurrentView(PageView.Books);
    private void ManageReturnsButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => SetCurrentView(PageView.Returns);
    private void ManageAccountsButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => SetCurrentView(PageView.Account);
    private void QuickAddBookButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => SetCurrentView(PageView.BookAdd);

    private void SetCurrentView(PageView targetView)
    {
        if (DataContext is MainPage_VM vm) vm.SetCurrentView(targetView);
    }

}
