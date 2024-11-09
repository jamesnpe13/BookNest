using BookNest.Data;
using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookNest.ViewModels;

public partial class RegistrationPage_VM : ObservableObject
{
    private readonly PageNavigationService ns;
    private readonly DatabaseService dbs;

    public RegistrationPage_VM(PageNavigationService _ns, DatabaseService _dbs)
    {
        ns = _ns;
        dbs = _dbs;
    }

    public void SubmitForm()
    {
        ns.SetCurrentPage("SignInPage");
    }

    public void RegisterAccount(Account_M tempAccount, string password)
    {
        byte[] salt = PasswordManager.GenerateSalt();
        string hashedPassword = PasswordManager.HashPassword(password, salt);
        tempAccount.PasswordHash = hashedPassword;
        tempAccount.Salt = Convert.ToBase64String(salt);

        Console.WriteLine(tempAccount.FirstName);
        Console.WriteLine(tempAccount.LastName);
        Console.WriteLine(tempAccount.Username);
        Console.WriteLine(tempAccount.Email);
        Console.WriteLine(tempAccount.PasswordHash);
        Console.WriteLine(tempAccount.Salt);
        Console.WriteLine(tempAccount.AccountType);

        // store account to database
        dbs.AddAccount(tempAccount);
    }
}