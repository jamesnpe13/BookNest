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
    // Page entry point (Default page)
    public string DefaultPage { get => "RegistrationPage"; }

    // currently signed in user
    [ObservableProperty]
    private Account_M currentAccount;

    // data collections

    public AppData()
    {

    }

}
