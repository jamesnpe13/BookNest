using CommunityToolkit.Mvvm.ComponentModel;

namespace BookNest.Models;

public partial class Account_M : ObservableObject
{
    [ObservableProperty] private int? accountId;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string username = string.Empty;
    [ObservableProperty] private string password = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string passwordHash = string.Empty;
    [ObservableProperty] private string salt = string.Empty;
    [ObservableProperty] private string accountType = string.Empty;
}
