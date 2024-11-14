using BookNest.Models;
using BookNest.ViewModels;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Views
{
    public partial class Book_AddUpdate_V : UserControl
    {
        private readonly Books_AddUpdate_VM booksAddUpdateVM;

        public Book_AddUpdate_V(Books_AddUpdate_VM _booksAddUpdateVM)
        {
            InitializeComponent();
            booksAddUpdateVM = _booksAddUpdateVM;
            DataContext = booksAddUpdateVM;
            CreateComboboxItems();
        }

        private void CreateComboboxItems()
        {
            var genreList = Enum.GetValues(typeof(BookGenre));

            foreach (var genre in genreList)
            {
                ComboBoxItem tempCBItem = new() { Content = genre.ToString() };
                GenreDropdown.DropdownCombobox.Items.Add(tempCBItem);
            }
        }

        private void SaveButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is Books_AddUpdate_VM vm)
            {
                vm.SubmitForm();
            }
        }

        private void CancelButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is Books_AddUpdate_VM vm)
            {
                vm.MainPageVM.NavigateBack();
            }
        }

        private void CancelButtonUtility_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is Books_AddUpdate_VM vm)
            {
                vm.MainPageVM.NavigateBack();
            }
        }

        private void submitForm()
        {

        }

        private void GenreDropdown_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is Books_AddUpdate_VM vm)
            {
                if (GenreDropdown.DropdownCombobox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string selectedValue = selectedItem.Content.ToString();
                    vm.BookGenre = selectedValue;
                }
            }
        }
    }
}
