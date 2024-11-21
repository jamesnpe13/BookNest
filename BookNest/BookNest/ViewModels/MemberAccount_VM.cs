using BookNest.Models;
using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MemberAccount_VM : ObservableObject
{
    private readonly MainPage_VM vm;

    [ObservableProperty] private UserControl currentView;
    [ObservableProperty] private Visibility isNoResultsMessageVisible = Visibility.Collapsed;
    [ObservableProperty] private string currentPageTitle = string.Empty;

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

    public MemberAccount_VM()
    {
        BookList = new();
        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
        vm.PropertyChanged += OnCurrentViewChanged;
        SetCurentView("Account");
    }

    public void SetCurentView(string targetView)
    {
        if (targetView == "Account") CurrentView = ((App)Application.Current).ServiceProvider.GetRequiredService<AccountTab_V>();
        if (targetView == "LoanedBooks") CurrentView = ((App)Application.Current).ServiceProvider.GetRequiredService<LoanedTab_V>();
        if (targetView == "Reserved") CurrentView = ((App)Application.Current).ServiceProvider.GetRequiredService<ReservedTab_V>();
        if (targetView == "History") CurrentView = ((App)Application.Current).ServiceProvider.GetRequiredService<HistoryTab_V>();
    }

    public void OnCurrentViewChanged(object sender, PropertyChangedEventArgs e)
    {
        if (vm.CurrentView.GetType() == typeof(Member_Account_V)) SetCurentView("Account");
    }

    partial void OnCurrentViewChanged(UserControl? oldValue, UserControl newValue)
    {
        if (currentView != null)
        {
            if (CurrentView.GetType() == typeof(AccountTab_V)) this.CurrentPageTitle = "Account details";
            if (CurrentView.GetType() == typeof(LoanedTab_V)) this.CurrentPageTitle = "Loaned books";
            if (CurrentView.GetType() == typeof(ReservedTab_V)) this.CurrentPageTitle = "Reserved books";
            if (CurrentView.GetType() == typeof(HistoryTab_V)) this.CurrentPageTitle = "Borrowing history";
        }
    }

}
