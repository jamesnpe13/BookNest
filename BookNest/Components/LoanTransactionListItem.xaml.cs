using BookNest.Data;
using BookNest.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookNest.Components;

public partial class LoanTransactionListItem : UserControl
{
    private readonly MainPage_VM vm;
    private readonly DatabaseService ds;

    public LoanTransactionListItem()
    {
        InitializeComponent();
        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
        ds = ((App)Application.Current).ServiceProvider.GetRequiredService<DatabaseService>();
    }

    public string Title
    {
        get
        {
            string thisTitle = ds.GetBook(BookFilterKey.ID, (string)BookId)[0].Title;
            return thisTitle;
        }

    }

    public string Author
    {
        get
        {
            string thisAuthor = ds.GetBook(BookFilterKey.ID, (string)BookId)[0].Author;
            return thisAuthor;
        }

    }

    public string IsOverdue
    {
        get
        {
            if (ds.GetLoanTransaction(LoanTransactionFilterKey.TRANSACTION_ID, (string)LoanTransactionId)[0].IsOverdue)
            {
                return "Overdue";
            }
            else
            {
                int daysRemaining = ds.GetLoanTransaction(LoanTransactionFilterKey.TRANSACTION_ID, (string)LoanTransactionId)[0].GetRemainingDays();
                return $"Due in {daysRemaining} days";
            }
        }
    }

    public string LoanTransactionId
    {
        get { return (string)GetValue(LoanTransactionIdProperty); }
        set { SetValue(LoanTransactionIdProperty, value); }
    }

    public static readonly DependencyProperty LoanTransactionIdProperty =
        DependencyProperty.Register("LoanTransactionId", typeof(string), typeof(LoanTransactionListItem), new PropertyMetadata(string.Empty));

    public string AccountId
    {
        get { return (string)GetValue(AccountIdProperty); }
        set { SetValue(AccountIdProperty, value); }
    }

    public static readonly DependencyProperty AccountIdProperty =
        DependencyProperty.Register("AccountId", typeof(string), typeof(LoanTransactionListItem), new PropertyMetadata(string.Empty));

    public string BookId
    {
        get { return (string)GetValue(BookIdProperty); }
        set { SetValue(BookIdProperty, value); }
    }

    public static readonly DependencyProperty BookIdProperty =
        DependencyProperty.Register("BookId", typeof(string), typeof(LoanTransactionListItem), new PropertyMetadata(string.Empty));

    public string LoanDate
    {
        get { return (string)GetValue(LoanDateProperty); }
        set { SetValue(LoanDateProperty, value); }
    }

    public static readonly DependencyProperty LoanDateProperty =
         DependencyProperty.Register("LoanDate", typeof(string), typeof(LoanTransactionListItem), new PropertyMetadata(string.Empty));

    public string DueDate
    {
        get { return (string)GetValue(DueDateProperty); }
        set { SetValue(DueDateProperty, value); }
    }

    public static readonly DependencyProperty DueDateProperty =
         DependencyProperty.Register("DueDate", typeof(string), typeof(LoanTransactionListItem), new PropertyMetadata(string.Empty));

    public string Status
    {
        get { return (string)GetValue(StatusProperty); }
        set { SetValue(StatusProperty, value); }
    }

    public static readonly DependencyProperty StatusProperty =
         DependencyProperty.Register("Status", typeof(string), typeof(LoanTransactionListItem), new PropertyMetadata(string.Empty));
}
