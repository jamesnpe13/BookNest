using BookNest.Data;
using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookNest.ViewModels;

public partial class RegistrationPage_VM : ObservableObject
{
    private readonly PageNavigationService ns;
    private readonly DatabaseService dbs;
    private readonly PasswordManager pm;

    public RegistrationPage_VM(PageNavigationService _ns, DatabaseService _dbs, PasswordManager _pm)
    {
        ns = _ns;
        dbs = _dbs;
        pm = _pm;
    }

    public void SubmitForm()
    {
        ns.SetCurrentPage("SignInPage");
    }

    public void RegisterAccount(Account_M tempAccount, string password)
    {

        byte[] salt = pm.GenerateSalt();
        string hashedPassword = pm.HashPassword(password, salt);
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