using CommunityToolkit.Mvvm.ComponentModel;

namespace BookNest.Models;

public partial class Account_M : ObservableObject
{
    [ObservableProperty]
    private string firstName;

    [ObservableProperty]
    private string lastName;

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string passwordHash;

    [ObservableProperty]
    private string salt;

    [ObservableProperty]
    private string accountType;
}
