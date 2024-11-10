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

    // User sign out
}
