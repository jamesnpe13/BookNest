using BookNest.Models;
using BookNest.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Views;

public partial class Admin_Books_V : UserControl
{
    private readonly MainPage_VM vm;

    public Admin_Books_V(MainPage_VM _vm)
    {
        vm = _vm;
        InitializeComponent();
        CreateComboboxItems();
        DataContext = vm;
    }

    private void backButtonUtility_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.NavigateBack();
        }
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
}
