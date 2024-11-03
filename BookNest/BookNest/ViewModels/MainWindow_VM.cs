using BookNest.Pages;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace BookNest.ViewModels;

partial class MainWindow_VM : ObservableObject
{

    [ObservableProperty] private object currentPage; // bound to Main Frame that displays current page
    [ObservableProperty] private string targetPage;

    public MainWindow_VM()
    {
        WeakReferenceMessenger.Default.Register<NavigateToPage_Message>(this, (r, page) =>
        {
            TargetPage = page.TargetPage;
        });

        SetCurrentPage("MainPage"); // set default page
    }

    // Page router
    [RelayCommand]
    public void SetCurrentPage(string page)
    {
        if (page == "MainPage") CurrentPage = new MainPage();
        if (page == "SignInPage") CurrentPage = new SignInPage();
        if (page == "RegistrationPage") CurrentPage = new RegistrationPage();
    }
}
