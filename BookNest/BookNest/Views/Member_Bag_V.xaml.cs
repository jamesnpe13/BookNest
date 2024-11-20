using BookNest.Data;
using BookNest.Models;
using BookNest.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Views;

public partial class Member_Bag_V : UserControl
{
    private readonly MainPage_VM vm;
    private readonly DatabaseService ds;

    public Member_Bag_V()
    {
        InitializeComponent();
        CreateComboboxItems();

        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
        ds = ((App)Application.Current).ServiceProvider.GetRequiredService<DatabaseService>();

        DisplayBagItems();
        vm.UpdateIsNoResultsVisible();

    }

    private void DisplayBagItems()
    {
        vm.BookList.Clear();
        foreach (var item in vm.BookBag)
        {
            vm.BookList.Add(ds.GetBook(BookFilterKey.ID, item.ToString())[0]);
        }
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

    private void CheckoutButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        vm.CheckoutBooks();
    }

    //private void RemoveAllButton_MouseDown(object sender, MouseButtonEventArgs e)
    //{
    //    vm.BookBag.Clear();
    //    vm.RefreshBookList();

    //}
}