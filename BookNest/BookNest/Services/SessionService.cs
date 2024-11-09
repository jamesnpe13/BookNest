using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Services;

public partial class SessionService : ObservableObject
{
    public SessionService()
    {

    }

    public Account_M CreateTempAccount(string name)
    {
        return new Account_M()
        {
            FirstName = name
        };
    }
}
