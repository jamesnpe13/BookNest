using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;

namespace BookNest.Models;

public enum BookGenre
{
    Unassigned,
    ScienceFiction,
    Fiction,
    NonFiction,
}

public partial class Book_M : ObservableObject
{
    [ObservableProperty]
    private int? bookId;

    [ObservableProperty]
    private string isbn = string.Empty;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string author = string.Empty;

    [ObservableProperty]
    private BookGenre genre = BookGenre.Unassigned;

    [ObservableProperty]
    private string yearOfPublication = string.Empty;

    [ObservableProperty]
    private string publisher = string.Empty;

    [ObservableProperty]
    private int likes = 0;

    // add more props if needed...

}
