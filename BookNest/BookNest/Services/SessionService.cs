using BookNest.Data;
using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Services;

public partial class SessionService : ObservableObject
{
    private readonly DatabaseService ds;
    private readonly PasswordManager pm;
    private readonly AppData ad;
    private readonly PageNavigationService ns;

    public SessionService(DatabaseService _ds, PasswordManager _pm, AppData _ad, PageNavigationService _ns)
    {
        ds = _ds;
        pm = _pm;
        ad = _ad;
        ns = _ns;
    }

    // Handle user sign in
    public void HandleUserSignIn(string usernameInput, string passwordInput, string accountType)
    {
        // get user from db

        Account_M thisAccount = ds.GetAccount(usernameInput, accountType, true);
        Console.WriteLine("Account found: " + thisAccount.FirstName + " " + thisAccount.LastName);
        //Console.WriteLine("Password verified: " + pm.VerifyPassword(passwordInput, thisAccount));

        // call password verify from password manager
        if (pm.VerifyPassword(passwordInput, thisAccount))
        {
            ad.CurrentAccount = thisAccount;
            ns.SetCurrentPage("MainPage");
        }
    }

    // handle user sign out
    public void HandleUserSignOut()
    {
        ad.CurrentAccount = null;
        ns.SetCurrentPage("SignInPage");
    }

    // handle user update account
    public void UpdateAccount(string targetUsername, string targetAccountType, Account_M updatedAccount)
    {
        // get the account
        Account_M targetAccount = ds.GetAccount(targetUsername, targetAccountType, true);
        string thisUsername = targetAccount.Username;
        string thisAccountType = targetAccount.AccountType;

        // create temp account dupe for mod
        Account_M tempAccount = ds.GetAccount(targetUsername, targetAccountType, true);

        // modify fields - check if updated fields are empty or null or if value same as existing
        if (!string.IsNullOrEmpty(updatedAccount.FirstName) && updatedAccount.FirstName != targetAccount.FirstName)
            tempAccount.FirstName = updatedAccount.FirstName;
        if (!string.IsNullOrEmpty(updatedAccount.LastName) && updatedAccount.LastName != targetAccount.LastName)
            tempAccount.LastName = updatedAccount.LastName;
        if (!string.IsNullOrEmpty(updatedAccount.Username) && updatedAccount.Username != targetAccount.Username)
            tempAccount.Username = updatedAccount.Username;
        if (!string.IsNullOrEmpty(updatedAccount.Password) && updatedAccount.Password != targetAccount.Password)
            tempAccount.Password = updatedAccount.Password;
        if (!string.IsNullOrEmpty(updatedAccount.PasswordHash) && updatedAccount.PasswordHash != targetAccount.PasswordHash)
            tempAccount.PasswordHash = updatedAccount.PasswordHash;
        if (!string.IsNullOrEmpty(updatedAccount.Salt) && updatedAccount.Salt != targetAccount.Salt)
            tempAccount.Salt = updatedAccount.Salt;
        if (!string.IsNullOrEmpty(updatedAccount.Email) && updatedAccount.Email != targetAccount.Email)
            tempAccount.Email = updatedAccount.Email;
        if (!string.IsNullOrEmpty(updatedAccount.AccountType) && updatedAccount.AccountType != targetAccount.AccountType)
            tempAccount.AccountType = updatedAccount.AccountType;

        // update db
        ds.UpdateAccount(thisUsername, thisAccountType, tempAccount);
    }
}
