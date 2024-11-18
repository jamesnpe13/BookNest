using BookNest.Models;
using BookNest.Services;
using BookNest.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.Modules;

public partial class BookSingle : UserControl
{
    private readonly MainPage_VM vm;

    public BookSingle()
    {
        InitializeComponent();
        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
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

    private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        NavigateBack();
    }

    private void backButtonUtility_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        NavigateBack();
    }

    private void NavigateBack()
    {
        vm.IsEditing = false;
        vm.CurrentBook = null;
    }

    private void EditButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        vm.IsEditing = true;
    }
}
