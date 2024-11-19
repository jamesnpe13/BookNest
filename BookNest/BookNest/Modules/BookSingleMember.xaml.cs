using BookNest.Data;
using BookNest.Models;
using BookNest.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.Modules;

public partial class BookSingleMember : UserControl
{
    private readonly MainPage_VM vm;
    private readonly DatabaseService ds;

    public BookSingleMember()
    {
        InitializeComponent();
        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
        ds = ((App)Application.Current).ServiceProvider.GetRequiredService<DatabaseService>();
        CreateComboboxItems();
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

    private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => NavigateBack();

    private void backButtonUtility_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => NavigateBack();

    private void NavigateBack()
    {
        vm.IsEditing = false;
        vm.CurrentBook = null;
    }

    private void DynamicButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        vm.BookBag.Add(vm.CurrentBook.BookId);
    }
}
