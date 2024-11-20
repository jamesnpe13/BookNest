using BookNest.Data;
using BookNest.Models;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MainWindow_VM : ObservableObject
{

    private readonly PageNavigationService ns;
    private readonly AppData ad;

    [ObservableProperty]
    private string? targetPage;

    [ObservableProperty]
    private Page? currentPage;

    public MainWindow_VM(PageNavigationService _ns, AppData _ad)
    {
        ns = _ns;
        ad = _ad;

        ns.SetCurrentPage(ad.DefaultPage); // sets default page
    }

    [RelayCommand]
    public void NavigateToPage(string targetPage)
    {
        ns.SetCurrentPage(targetPage);

    }

}