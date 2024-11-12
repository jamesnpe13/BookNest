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
    private readonly SessionService ss;

    public RegistrationPage_VM(PageNavigationService _ns, DatabaseService _dbs, PasswordManager _pm, SessionService _ss)
    {
        ns = _ns;
        dbs = _dbs;
        pm = _pm;
        ss = _ss;
    }

    public void SubmitForm()
    {

    }

    public void RegisterAccount(Account_M tempAccount, string password = "")
    {
        byte[] salt = pm.GenerateSalt();
        string hashedPassword = pm.HashPassword(password, salt);
        tempAccount.PasswordHash = hashedPassword;
        tempAccount.Salt = Convert.ToBase64String(salt);

        dbs.AddAccount(tempAccount);
        ns.SetCurrentPage("SignInPage");
    }
}