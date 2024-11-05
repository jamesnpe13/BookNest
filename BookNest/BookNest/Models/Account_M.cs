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
    private string password;

    [ObservableProperty]
    private string accountType;

    [ObservableProperty]
    private int id;

    public Account_M()
    {
        GenerateId();
    }

    private void GenerateId()
    {
        Random random = new();
        int randomNum = random.Next(10000000, 100000000);
        this.Id = randomNum;
    }
}
