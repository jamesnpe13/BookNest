using BookNest.Models;
using BookNest.Services;
using BookNest.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Views;

public partial class Member_Books_V : UserControl
{
    private readonly MainPage_VM vm;

    public Member_Books_V()
    {
        InitializeComponent();
        CreateComboboxItems();

        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
        vm.UpdateBookList(BookFilterKey.ALL);

    }

    private void backButtonUtility_MouseDown(object sender, MouseButtonEventArgs e)
    {
        vm.NavigateBack();
    }

    private void CreateComboboxItems()
    {
        GenreDropdown.DropdownCombobox.Items.Add(new ComboBoxItem() { Content = "All" }); // add ALL BOOKS category

        var genreList = Enum.GetValues(typeof(BookGenre));

        foreach (var genre in genreList)
        {
            ComboBoxItem tempCBItem = new() { Content = genre.ToString() };
            GenreDropdown.DropdownCombobox.Items.Add(tempCBItem);
        }
    }

    private void AddBookButtonUtility_MouseDown(object sender, MouseButtonEventArgs e)
    {
        vm.SetCurrentView(PageView.BookAdd);
    }
}
