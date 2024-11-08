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

    [ObservableProperty] private string targetPage;
    [ObservableProperty] private Page currentPage;

    public MainWindow_VM(IServiceProvider _sp, PageNavigationService _ns, AppData _ad)
    {
        sp = _sp;
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
