using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace BookNest.ViewModels;

public partial class Books_AddUpdate_VM : ObservableObject
{
    [ObservableProperty]
    private MainPage_VM mainPageVM;

    [ObservableProperty] private string bookTitle = string.Empty;
    [ObservableProperty] private string bookAuthor = string.Empty;
    [ObservableProperty] private string bookISBN = string.Empty;
    [ObservableProperty] private string bookGenre = Models.BookGenre.Unassigned.ToString();
    [ObservableProperty] private string bookPublisher = string.Empty;
    [ObservableProperty] private string bookYearOfPublication = string.Empty;

    public Books_AddUpdate_VM(IServiceProvider _sp)
    {
        mainPageVM = _sp.GetRequiredService<MainPage_VM>();
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

}