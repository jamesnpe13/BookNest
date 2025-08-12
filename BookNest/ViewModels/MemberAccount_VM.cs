using BookNest.Data;
using BookNest.Models;
using BookNest.Services;
using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MemberAccount_VM : ObservableObject
{
    private readonly MainPage_VM vm;
    private readonly DatabaseService ds;
    private readonly AppData ad;

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
    private ObservableCollection<LoanTransaction_M> loanTransactionList;
    public ObservableCollection<LoanTransaction_M> LoanTransactionList
    {
        get => loanTransactionList;
        set
        {
            if (loanTransactionList != value)
            {
                loanTransactionList = value;
                OnPropertyChanged(nameof(LoanTransactionList));
            }
        }
    }

    public MemberAccount_VM()
    {
        BookList = new();
        LoanTransactionList = new();
        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
        ds = ((App)Application.Current).ServiceProvider.GetRequiredService<DatabaseService>();
        ad = ((App)Application.Current).ServiceProvider.GetRequiredService<AppData>();
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
        BookList.Clear();
        if (vm.CurrentBook != null)
        {
            if (vm.CurrentView.GetType() == typeof(Member_Account_V))
            {
                SetCurentView("Account");
            }
        }
    }

    partial void OnCurrentViewChanged(UserControl? oldValue, UserControl newValue)
    {
        BookList.Clear();
        if (currentView != null)
        {
            if (CurrentView.GetType() == typeof(AccountTab_V)) this.CurrentPageTitle = "Account details";
            if (CurrentView.GetType() == typeof(LoanedTab_V))
            {
                this.CurrentPageTitle = "Loaned books";

                ObservableCollection<Book_M> tempBookList = new();
                ObservableCollection<LoanTransaction_M> tempTransactionList = new();
                tempTransactionList = ds.GetLoanTransaction(LoanTransactionFilterKey.ACCOUNT_ID, ad.CurrentAccount.AccountId.ToString());

                foreach (var transaction in tempTransactionList)
                {
                    tempBookList.Add(ds.GetBook(BookFilterKey.ID, transaction.BookId.ToString())[0]);
                }
                BookList.Clear();
                BookList = tempBookList;
                LoanTransactionList = tempTransactionList;

            }
            if (CurrentView.GetType() == typeof(ReservedTab_V)) this.CurrentPageTitle = "Reserved books";
            if (CurrentView.GetType() == typeof(HistoryTab_V))
            {
                this.CurrentPageTitle = "Borrowing history";
                LoanTransactionList.Clear();

                ObservableCollection<LoanTransaction_M> tempList = new();
                tempList = ds.GetLoanTransaction(LoanTransactionFilterKey.ALL);
                foreach (var item in tempList)
                {
                    if (item.AccountId == ad.CurrentAccount.AccountId)
                    {
                        LoanTransactionList.Add(item);
                    }
                }

            }
        }
    }

}
