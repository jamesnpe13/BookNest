﻿using BookNest.Data;
using BookNest.Models;
using BookNest.Services;
using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SharpVectors.Dom;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public enum PageView
{

    Dashboard,
    Bag,
    Books,
    Watchlist,
    Returns,
    Reserved,
    People,
    Account,
    SignOut,
    BookSingleDetail,
    BookAdd,
    BookUpdate
}

public partial class MainPage_VM : ObservableObject
{

    private readonly AppData ad;
    private readonly SessionService ss;
    private readonly IServiceProvider sp;
    private readonly DatabaseService ds;

    [ObservableProperty]
    private bool isEditing = false;

    [ObservableProperty]
    private Book_M? currentBook;

    [ObservableProperty]
    private UserControl currentView;

    [ObservableProperty]
    private string welcomeTextLine1 = string.Empty;

    [ObservableProperty]
    private string welcomeTextLine2 = string.Empty;

    [ObservableProperty]
    private string accountType = string.Empty;

    [ObservableProperty]
    private UserControl? lastView;

    [ObservableProperty]
    private string currentPageTitle = "All books";

    [ObservableProperty]
    private BookGenre? selectedGenre = null;

    [ObservableProperty]
    private Visibility isNoResultsMessageVisible = Visibility.Collapsed;

    private ObservableCollection<Book_M> bookList;

    public ObservableCollection<Book_M> BookList
    {
        get => bookList;
        set
        {
            if (bookList != value)
            {
                bookList = value;
                OnPropertyChanged(nameof(BookList));

            }
        }
    }

    [ObservableProperty] private Visibility dashboardNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility booksNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility bagNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility watchlistNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility returnsNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility reservedNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility peopleNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility accountNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility signOutNavButtonVisibility = Visibility.Collapsed;

    public MainPage_VM(AppData _ad, SessionService _ss, IServiceProvider _sp, DatabaseService _ds)
    {
        ad = _ad;
        ss = _ss;
        sp = _sp;
        ds = _ds;
        BookList = new();
        BookList = ds.GetBook(BookFilterKey.ALL);

        SessionService.UserSignedInOut += ResetInstance;
        //BookList.CollectionChanged += BookList_CollectionChanged;
        ResetInstance();
        UpdateIsNoResultsVisible();
    }

    //private void BookList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    //{
    //    NotificationService.Instance.AddNotificationItem(Components.NotificationToastStyle.Default, "Changing");

    //    if (e.Action == NotifyCollectionChangedAction.Replace || e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Reset || e.Action == NotifyCollectionChangedAction.Remove)
    //    {
    //        IsNoResultsMessageVisible = BookList.Count() == 0 ? Visibility.Visible : Visibility.Collapsed;
    //    }
    //}

    public void SetCurrentBook(int bookId)
    {
        CurrentBook = ds.GetBook(BookFilterKey.ID, bookId.ToString())[0];
    }

    public void UpdateIsNoResultsVisible()
    {
        IsNoResultsMessageVisible = BookList.Count() == 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    public void FilterListByGenre(BookGenre value)
    {
        var filteredList = BookList.Where(book => book.Genre == value);

        BookList.Clear();

        foreach (var item in filteredList)
        {
            BookList.Add(item);
        }
    }

    public void UpdateBookList(BookFilterKey filterKey, string? value = null)
    {
        BookList = ds.GetBook(filterKey, value);
        CurrentPageTitle = value == null ? "All books" : $"'Search: {value}'";
        UpdateIsNoResultsVisible();
    }

    // navbar style (member or admin)
    public void NavbarInit()
    {

        // set account type display
        AccountType = ad.CurrentAccount.AccountType;

        // show view buttons
        if (ad.CurrentAccount.AccountType == "Member")
        {
            DashboardNavButtonVisibility = Visibility.Visible;
            BooksNavButtonVisibility = Visibility.Visible;
            BagNavButtonVisibility = Visibility.Visible;
            WatchlistNavButtonVisibility = Visibility.Visible;
            ReturnsNavButtonVisibility = Visibility.Collapsed;
            ReservedNavButtonVisibility = Visibility.Collapsed;
            PeopleNavButtonVisibility = Visibility.Collapsed;
            AccountNavButtonVisibility = Visibility.Visible;
            SignOutNavButtonVisibility = Visibility.Visible;
        }
        if (ad.CurrentAccount.AccountType == "Administrator")
        {
            DashboardNavButtonVisibility = Visibility.Visible;
            BooksNavButtonVisibility = Visibility.Visible;
            BagNavButtonVisibility = Visibility.Collapsed;
            WatchlistNavButtonVisibility = Visibility.Collapsed;
            ReturnsNavButtonVisibility = Visibility.Visible;
            ReservedNavButtonVisibility = Visibility.Visible;
            PeopleNavButtonVisibility = Visibility.Visible;
            AccountNavButtonVisibility = Visibility.Visible;
            SignOutNavButtonVisibility = Visibility.Visible;
        }
    }

    public void InitPageData()
    {
        // set welcome message
        WelcomeTextLine1 = "Hi, " + ad.CurrentAccount.FirstName ?? "User";
        WelcomeTextLine2 = "Let's get started";
    }

    // setting page views
    public void SetCurrentView(PageView targetView)
    {
        // (Shared buttons) dynamic views - checks current user's account type then display's correct view
        if (targetView == PageView.Dashboard)
        {
            CurrentView = ad.CurrentAccount.AccountType == "Administrator" ? sp.GetRequiredService<Admin_Dashboard_V>() : sp.GetRequiredService<Member_Dashboard_V>();
        }
        if (targetView == PageView.Account)
        {
            CurrentView = ad.CurrentAccount.AccountType == "Administrator" ? sp.GetRequiredService<Admin_Account_V>() : sp.GetRequiredService<Member_Account_V>();
        }
        if (targetView == PageView.Books)
        {
            CurrentView = ad.CurrentAccount.AccountType == "Administrator" ? sp.GetRequiredService<Admin_Books_V>() : sp.GetRequiredService<Member_Books_V>();
        }

        // admin specific views
        if (targetView == PageView.People) CurrentView = sp.GetRequiredService<Admin_People_V>();
        if (targetView == PageView.Reserved) CurrentView = sp.GetRequiredService<Admin_Reserved_V>();
        if (targetView == PageView.Returns) CurrentView = sp.GetRequiredService<Admin_Returns_V>();

        // member specific views
        if (targetView == PageView.Bag) CurrentView = sp.GetRequiredService<Member_Bag_V>();
        if (targetView == PageView.Watchlist) CurrentView = sp.GetRequiredService<Member_Watchlist_V>();

        // Book - Single views
        if (targetView == PageView.BookSingleDetail) CurrentView = sp.GetRequiredService<Book_Details_V>();
        if (targetView == PageView.BookAdd) CurrentView = sp.GetRequiredService<Book_AddUpdate_V>();
        if (targetView == PageView.BookUpdate) CurrentView = sp.GetRequiredService<Book_AddUpdate_V>();

    }

    // set last page view
    partial void OnCurrentViewChanged(UserControl? oldValue, UserControl? newValue)
    {
        LastView = oldValue;
    }

    public void NavigateBack()
    {
        Console.WriteLine("Navigating back to: ");
        CurrentView = LastView;
    }

    public void HandleUserSignOut()
    {

        ss.HandleUserSignOut();
    }

    public void ResetInstance()
    {

        CurrentView = null;

        WelcomeTextLine1 = string.Empty;

        WelcomeTextLine2 = string.Empty;

        CurrentBook = null;

        AccountType = string.Empty;

        LastView = null;

        CurrentPageTitle = "All books";

        DashboardNavButtonVisibility = Visibility.Collapsed;
        BooksNavButtonVisibility = Visibility.Collapsed;
        BagNavButtonVisibility = Visibility.Collapsed;
        WatchlistNavButtonVisibility = Visibility.Collapsed;
        ReturnsNavButtonVisibility = Visibility.Collapsed;
        ReservedNavButtonVisibility = Visibility.Collapsed;
        PeopleNavButtonVisibility = Visibility.Collapsed;
        AccountNavButtonVisibility = Visibility.Collapsed;
        SignOutNavButtonVisibility = Visibility.Collapsed;

        NavbarInit(); // initialize data on side navbar
        InitPageData();
        SetCurrentView(ad.DefaultView); // set default page view

        Console.WriteLine("Instance reset.");
    }
}
