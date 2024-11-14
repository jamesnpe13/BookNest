using BookNest.Data;
using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace BookNest.ViewModels;

public partial class Books_AddUpdate_VM : ObservableObject
{
    [ObservableProperty]
    private MainPage_VM mainPageVM;
    private DatabaseService ds;

    [ObservableProperty] private string bookTitle = string.Empty;
    [ObservableProperty] private string bookAuthor = string.Empty;
    [ObservableProperty] private string bookISBN = string.Empty;
    [ObservableProperty] private string bookGenre = Models.BookGenre.Unassigned.ToString();
    [ObservableProperty] private string bookPublisher = string.Empty;
    [ObservableProperty] private string bookYearOfPublication = string.Empty;

    public Books_AddUpdate_VM(IServiceProvider _sp, MainPage_VM _mainPageVM, DatabaseService _ds)
    {
        MainPageVM = _mainPageVM;
        ds = _ds;
    }

    // displays default text if textboxes are null or empty
    partial void OnBookTitleChanged(string? oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(newValue)) BookTitle = "Untitled Book";
    }
    partial void OnBookAuthorChanged(string? oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(newValue)) bookAuthor = "Unknown author";
    }
    partial void OnBookISBNChanged(string? oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(newValue)) BookISBN = "ISBN";
    }
    partial void OnBookGenreChanged(string? oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(newValue)) BookGenre = "Genre";
    }
    partial void OnBookYearOfPublicationChanged(string? oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(newValue)) BookYearOfPublication = "Publication year";
    }

    public void SubmitForm()
    {
        try
        {
            Book_M tempBook = new()
            {
                Title = BookTitle,
                Author = BookAuthor,
                Isbn = BookISBN,
                Genre = Enum.Parse<BookGenre>(BookGenre),
                Publisher = BookPublisher,
                YearOfPublication = BookYearOfPublication
            };

            ds.AddBook(tempBook);

            Console.WriteLine("Successfully added book entry");

            MainPageVM.NavigateBack();
        }
        catch (Exception err)
        {
            Console.WriteLine("Failed to add book: " + err.Message);
        }
    }

}