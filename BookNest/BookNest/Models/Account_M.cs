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
    private string password;

    [ObservableProperty]
    private string accountType;

    [ObservableProperty]
    private int id;
}
