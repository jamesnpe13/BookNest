using BookNest.Pages;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public partial class MainWindow_VM : ObservableObject
{
    private readonly IServiceProvider sp;

    [ObservableProperty] private Page currentPage; // bound to Main Frame that displays current page
    [ObservableProperty] private string targetPage;

    public MainWindow_VM(IServiceProvider _sp)
    {
        sp = _sp;

        SetCurrentPage("MainPage"); // set default page
    }

    // Page router
    [RelayCommand]
    public void SetCurrentPage(string page)
    {
        if (page == "MainPage") CurrentPage = sp.GetRequiredService<MainPage>();
        if (page == "SignInPage") CurrentPage = sp.GetRequiredService<SignInPage>();
        if (page == "RegistrationPage") CurrentPage = sp.GetRequiredService<RegistrationPage>();
    }
}
