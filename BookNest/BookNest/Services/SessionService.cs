﻿using BookNest.Data;
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

    public SessionService(DatabaseService _ds, PasswordManager _pm, AppData _ad)
    {
        ds = _ds;
        pm = _pm;
        ad = _ad;
    }

    // create temp account
    public Account_M CreateTempAccount(string name)
    {
        return new Account_M()
        {
            FirstName = name
        };
    }

    // Handle user sign in
    public void HandleUserSignIn(string usernameInput, string passwordInput, string accountType)
    {
        // get user from db
        Account_M thisAccount = ds.GetAccount_single(usernameInput, accountType);

        // get user password hash and salt
        string thisPasswordHash = thisAccount.PasswordHash;
        byte[] thisSalt = Encoding.UTF8.GetBytes(thisAccount.Salt);

        Console.WriteLine("PW salt string: " + thisSalt);

        // call password verify from password manager
        Console.WriteLine("Password verified: " + pm.VerifyPassword(passwordInput, thisSalt, thisPasswordHash));
    }

    // handle user sign out

    // handle user update account
    public void UpdateAccount(string targetUsername, string targetAccountType, Account_M updatedAccount)
    {
        Console.WriteLine("Updating account");

        // get the account
        Account_M targetAccount = ds.GetAccount_single(targetUsername, targetAccountType);
        string thisUsername = targetAccount.Username;
        string thisAccountType = targetAccount.AccountType;

        // create temp account dupe for mod
        Account_M tempAccount = ds.GetAccount_single(targetUsername, targetAccountType);

        // modify fields - check if updated fields are empty or null or if value same as existing
        if (!string.IsNullOrEmpty(updatedAccount.FirstName) && updatedAccount.FirstName != targetAccount.FirstName)
            tempAccount.FirstName = updatedAccount.FirstName;
        if (!string.IsNullOrEmpty(updatedAccount.LastName) && updatedAccount.LastName != targetAccount.LastName)
            tempAccount.LastName = updatedAccount.LastName;
        if (!string.IsNullOrEmpty(updatedAccount.Username) && updatedAccount.Username != targetAccount.Username)
            tempAccount.Username = updatedAccount.Username;
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
