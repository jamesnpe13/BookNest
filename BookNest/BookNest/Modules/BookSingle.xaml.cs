using BookNest.Data;
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
    private readonly DatabaseService ds;

    public BookSingle()
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

    private void ConfirmButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Book_M tempBook = vm.CurrentBook;

        if (TitleField.Text != vm.CurrentBook.Title) tempBook.Title = TitleField.Text;
        if (AuthorField.Text != vm.CurrentBook.Author) tempBook.Author = AuthorField.Text;
        if (PublisherField.Text != vm.CurrentBook.Publisher) tempBook.Publisher = PublisherField.Text;
        if (YearOfPublicationField.Text != vm.CurrentBook.YearOfPublication) tempBook.YearOfPublication = YearOfPublicationField.Text;
        if (IsbnField.Text != vm.CurrentBook.Isbn) tempBook.Isbn = IsbnField.Text;

        try
        {
            ds.UpdateBook(vm.CurrentBook.BookId.ToString(), tempBook);
            NavigateBack();
            NotificationService.Instance.AddNotificationItem(Components.NotificationToastStyle.Success, "Successfuly updated book.");
        }
        catch (Exception err)
        {
            NotificationService.Instance.AddNotificationItem(Components.NotificationToastStyle.Warning, $"There was a problem updating book details. {err.Message}");
        }
    }

    private void DeleteButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            ds.DeleteBook(vm.CurrentBook.BookId.ToString());
            NavigateBack();
            NotificationService.Instance.AddNotificationItem(Components.NotificationToastStyle.Success, "Successfuly updated book.");
        }
        catch (Exception err)
        {
            NotificationService.Instance.AddNotificationItem(Components.NotificationToastStyle.Warning, $"There was a problem deleting book. {err.Message}");

        }
    }
}
