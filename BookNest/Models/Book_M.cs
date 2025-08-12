using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;

namespace BookNest.Models;

public enum BookGenre
{
    Unassigned,
    Adventure,
    Fantasy,
    ScienceFiction,
    Dystopian,
    Mystery,
    Thriller,
    Horror,
    Romance,
    HistoricalFiction,
    ContemporaryFiction,
    LiteraryFiction,
    BiographyAutobiography,
    Memoir,
    SelfHelp,
    TrueCrime,
    History,
    Science,
    Travel,
    Philosophy,
    Psychology,
    ReligionSpirituality,
    HealthAndWellness,
    Essays,
    Politics,
    Business,
    Poetry,
    YoungAdult,
    ChildrensLiterature,
    Western,
    Gothic
}

public enum BookStatus
{
    Available,
    Unavailable,
    CheckedOut,
    OnHold,
    Overdue,
    Lost,
    Reserved
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
    private BookStatus status = BookStatus.Available;

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
