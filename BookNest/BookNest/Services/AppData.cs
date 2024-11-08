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
    private string testString = "This is the test string";

    [ObservableProperty]
    private string welcomeTextLine1;

    [ObservableProperty]
    private string welcomeTextLine2;

    // data collections
    public ObservableCollection<Account_M> AccountsCollection;
    public ObservableCollection<Book_M> BooksCollection;

    public AppData()
    {
        AccountsCollection = new();

        InitTempAccount();
        ConstructWelcomeLine();
    }

    private void InitTempAccount()
    {
        AccountsCollection.Add(new Account_M
        {
            FirstName = "James",
            LastName = "Elazegui,"
        });

        CurrentAccount = AccountsCollection[0];
    }

    private void ConstructWelcomeLine()
    {
        WelcomeTextLine1 = "Hi, " + CurrentAccount.FirstName;
        WelcomeTextLine2 = "Let's get started!";
    }
}
