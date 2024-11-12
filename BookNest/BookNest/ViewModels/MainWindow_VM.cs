using BookNest.Data;
using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MainWindow_VM : ObservableObject
{
    private readonly IServiceProvider sp;
    private readonly PageNavigationService ns;
    private readonly AppData ad;
    private readonly DatabaseService ds;
    private readonly SessionService ss;

    [ObservableProperty]
    private string? targetPage;

    [ObservableProperty]
    private Page? currentPage;

    public MainWindow_VM(IServiceProvider _sp, PageNavigationService _ns, AppData _ad, DatabaseService _ds, SessionService _ss)
    {
        sp = _sp;
        ns = _ns;
        ad = _ad;
        ds = _ds;
        ss = _ss;

        ns.SetCurrentPage(ad.DefaultPage); // sets default page

        //TestNewBook();
        //TestGetBook();
    }

    [RelayCommand]
    public void NavigateToPage(string targetPage)
    {
        ns.SetCurrentPage(targetPage);
    }

    public void TestNewBook()
    {
        try
        {
            Book_M tempBook = new()
            {
                Isbn = "asd123",
                Title = "This is a Test Book",
                Genre = BookGenre.NonFiction,
                Author = "James Elazegui",
                YearOfPublication = "2024",
                Publisher = "Yoobee College",
                Likes = 150,
            };
            ds.AddBook(tempBook);

            Console.WriteLine("'TestNewBook' SUCCESS");
        }
        catch (Exception err)
        {
            Console.WriteLine("Cannot execute 'TestNewBook'");
            Console.WriteLine(err.Message);
        }

    }

    public void TestGetBook()
    {
        try
        {
            var thisBook = new Book_M();
            thisBook = ds.GetBook(DatabaseService.BookFilterKey.ID, "1", true);
        }
        catch (Exception err)
        {
            Console.WriteLine("FAILED to get book");
            Console.WriteLine(err.Message);
        }
    }
}
