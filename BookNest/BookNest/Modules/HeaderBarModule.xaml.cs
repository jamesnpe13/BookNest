using BookNest.Pages;
using BookNest.Services;
using BookNest.ViewModels;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Modules;

public partial class HeaderBarModule : UserControl
{
    public HeaderBarModule() => InitializeComponent();

    private void SearchField_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            SearchBooks();
        }
    }

    private void SearchButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SearchBooks();
    }

    public void SearchBooks()
    {
        if (!string.IsNullOrEmpty(SearchField.Text))
        {
            if (DataContext is MainPage_VM vm)
            {
                vm.UpdateBookList(BookFilterKey.SEARCH, SearchField.Text);
            }
        }

    }

    private void AllBooksButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.UpdateBookList(BookFilterKey.ALL);
        }
    }
}
