using BookNest.Models;
using BookNest.ViewModels;
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
    //private static AppData _instance;
    //public static AppData Instance => _instance ??= new AppData();

    // connection string
    public readonly string DB_STRING = @"Data Source=booknest.db";

    // Page entry point (Default page)
    public string DefaultPage { get => "SignInPage"; }

    // Main page - default view for both admin and member
    public PageView DefaultView { get => PageView.Dashboard; }

    // currently signed in user
    [ObservableProperty]
    private Account_M? currentAccount = new();

    public int loanDaysMax = 28;
}
