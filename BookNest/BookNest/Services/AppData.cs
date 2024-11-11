using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Services;

public partial class AppData : ObservableObject
{
    // connection string
    public readonly string DB_STRING = @"Data Source=booknest.db";

    // Page entry point (Default page)
    public string DefaultPage { get => "SignInPage"; }

    // currently signed in user
    [ObservableProperty]
    private Account_M? currentAccount = new();

    public AppData()
    {
    }

}
