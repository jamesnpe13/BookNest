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

    public SessionService(DatabaseService _ds, PasswordManager _pm)
    {
        ds = _ds;
        pm = _pm;
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
    public void HandleSignIn(string inputUsername, string inputPassword)
    {
        Console.WriteLine("Handling sign in: ");
        Console.WriteLine("Input Username: ");
        Console.WriteLine("Input Password: ");

        // get username

        // get password input

        // get user from database

        // verify password match
    }

    // sUer sign out
}
