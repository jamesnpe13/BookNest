using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;

namespace BookNest.Models;

public enum Genre
{
    ScienceFiction,
    Fiction,
    NonFiction,
}

public partial class Book_M : ObservableObject
{

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private string publisher;

    [ObservableProperty]
    private Genre genre;

    [ObservableProperty]
    private DateAndTime pubishDate;

    [ObservableProperty]
    private int likes;

    [ObservableProperty]
    private int id;

    // add more props if needed...

    public Book_M()
    {
        GenerateId();
    }

    private void GenerateId()
    {
        Random random = new();
        int randomNum = random.Next(10000000, 100000000);
        this.Id = randomNum;
    }

}
