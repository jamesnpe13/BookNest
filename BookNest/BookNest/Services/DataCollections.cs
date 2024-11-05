using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Services;

public partial class DataCollections : ObservableObject
{
    // instance of DataCollections
    private static DataCollections _instance;
    public static DataCollections Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataCollections();
            }

            return _instance;
        }
    }

    // Collections
    public ObservableCollection<Account_M> AccountsCollection { get; set; }
    public ObservableCollection<Book_M> BooksCollection { get; set; }

    // CTOR
    private DataCollections()
    {
        AccountsCollection = new();
        BooksCollection = new();

        CreateTempObjects();
    }

    // create test book
    public void CreateTempObjects()
    {
        // add an account to collection
        AccountsCollection.Add(new Account_M
        {
            FirstName = "James",
            LastName = "Elazegui",
            Username = "jamesnpe13",
            Password = "password"
        });

        // add a book to collection
        BooksCollection.Add(new Book_M
        {
            Title = "Harry Potter",
            Description = "This is a harry potter book",
            Publisher = "J.K. Rowling",
            Genre = Genre.ScienceFiction,
            Likes = 526,
        });

        BooksCollection.Add(new Book_M
        {
            Title = "How to code in C#",
            Description = "This is a C# book",
            Publisher = "James Elazegui",
            Genre = Genre.NonFiction,
            Likes = 1056,
        });
    }
}
