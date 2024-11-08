using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Services;

public partial class AppData : ObservableObject
{
    // Page entry point (Default page)
    public string DefaultPage { get => "SignInPage"; }

    // currently signed in user
    [ObservableProperty]
    private Account_M currentAccount;

    [ObservableProperty]
    private string testString;

    // data collections
    public ObservableCollection<Account_M> AccountsCollection;
    public ObservableCollection<Book_M> BooksCollection;

    public AppData()
    {
        AccountsCollection = new();
    }

}
