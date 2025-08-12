using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BookNest.Models;

public enum LoanStatus
{
    OnLoan,
    Overdue,
    Returned
}

public partial class LoanTransaction_M : ObservableObject
{
    private readonly AppData ad;

    [ObservableProperty] private bool isOverdue = false;
    [ObservableProperty] private int? accountId;
    [ObservableProperty] private int? bookId;
    [ObservableProperty] private DateOnly dueDate;
    [ObservableProperty] private DateOnly loanDate;
    [ObservableProperty] private LoanStatus status = LoanStatus.OnLoan;
    [ObservableProperty] private int? loanTransactionId;
    public LoanTransaction_M()
    {
        ad = ((App)Application.Current).ServiceProvider.GetRequiredService<AppData>();
        LoanDate = SetLoanDate();
        DueDate = SetDueDate();
        isOverdue = CheckDue();
    }

    public bool CheckDue() => (GetRemainingDays() <= 0);
    public DateOnly SetLoanDate() => this.LoanDate = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly SetDueDate() => this.DueDate = this.LoanDate.AddDays(ad.loanDaysMax);
    public int GetRemainingDays() => DueDate.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber;

}
