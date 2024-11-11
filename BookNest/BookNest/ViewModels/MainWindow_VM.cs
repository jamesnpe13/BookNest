using BookNest.Data;
using BookNest.Models;
using BookNest.Pages;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MainWindow_VM : ObservableObject
{
    private readonly IServiceProvider sp;
    private readonly PageNavigationService ns;
    private readonly AppData ad;
    private readonly DatabaseService ds;

    [ObservableProperty] private string targetPage;
    [ObservableProperty] private Page currentPage;

    public MainWindow_VM(IServiceProvider _sp, PageNavigationService _ns, AppData _ad, DatabaseService _ds)
    {
        sp = _sp;
        ns = _ns;
        ad = _ad;
        ds = _ds;

        ns.SetCurrentPage(ad.DefaultPage); // sets default page
        ds.GetAccount_list();
        Console.WriteLine("USER ACCOUNT____________________________");
        testUpdateAccount();

    }

    [RelayCommand]
    public void NavigateToPage(string targetPage)
    {
        ns.SetCurrentPage(targetPage);
    }

    public void testUpdateAccount()
    {
        Console.WriteLine("Updating account");
        // get the account
        Account_M tempAccount = ds.GetAccount_single("jamese", "Administrator");
        string thisUsername = ds.GetAccount_single("jamese", "Administrator").Username;
        string thisAccountType = ds.GetAccount_single("jamese", "Administrator").AccountType;

        // modify fields
        tempAccount.FirstName = "JamesModified";
        tempAccount.LastName = "ElazeguiModified";

        // update db
        ds.UpdateAccount(thisUsername, thisAccountType, tempAccount);
    }
}
