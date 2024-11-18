using BookNest.Models;
using System.Windows.Controls;

namespace BookNest.Modules;

public partial class BookSingle : UserControl
{
    public BookSingle()
    {
        InitializeComponent();
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
}
