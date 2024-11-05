using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Services;

public partial class AppData : ObservableObject
{
    // instance of AppData
    private static AppData _instance;
    public static AppData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AppData();
            }

            return _instance;
        }
    }

    // Currently active account
    [ObservableProperty]
    public Account_M activeAccount;

    // Books collection

    // Accounts collection

    // CTOR
    private AppData()
    {
    }

    // sets active user
    public void SetActiveUser(Account_M account) => ActiveAccount = account;

}
