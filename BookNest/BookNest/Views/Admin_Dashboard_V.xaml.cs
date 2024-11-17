using BookNest.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

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

    private void DashboardSearchField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            SearchBooks();
        }
    }

    private void DashboardSearchButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SearchBooks();
    }

    public void SearchBooks()
    {
        if (!string.IsNullOrEmpty(DashboardSearchField.Text))
        {
            if (DataContext is MainPage_VM vm)
            {
                vm.UpdateBookList(BookFilterKey.SEARCH, DashboardSearchField.Text);
                vm.SetCurrentView(PageView.Books);
            }
        }
    }
}
