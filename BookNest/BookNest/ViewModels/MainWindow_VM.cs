using BookNest.Data;
using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MainWindow_VM : ObservableObject
{
    private readonly IServiceProvider sp;
    private readonly PageNavigationService ns;
    private readonly AppData ad;
    private readonly DatabaseService ds;
    private readonly SessionService ss;

    [ObservableProperty]
    private string? targetPage;

    [ObservableProperty]
    private Page? currentPage;

    public MainWindow_VM(IServiceProvider _sp, PageNavigationService _ns, AppData _ad, DatabaseService _ds, SessionService _ss)
    {
        sp = _sp;
        ns = _ns;
        ad = _ad;
        ds = _ds;
        ss = _ss;

        ns.SetCurrentPage(ad.DefaultPage); // sets default page

        // admin and member switching for testing
        //int tempUser = 1;

        //if (tempUser == 1) ss.HandleUserSignIn("admin", "123", "Administrator");
        //if (tempUser == 2) ss.HandleUserSignIn("member", "123", "Member");

    }

    [RelayCommand]
    public void NavigateToPage(string targetPage)
    {
        ns.SetCurrentPage(targetPage);
    }

}