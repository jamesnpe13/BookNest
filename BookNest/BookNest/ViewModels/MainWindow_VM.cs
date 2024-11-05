using BookNest.Pages;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

partial class MainWindow_VM : ObservableObject
{

    [ObservableProperty] private Object currentPage; // bound to Main Frame that displays current page
    [ObservableProperty] private string targetPage;
    [ObservableProperty] private string currentUser;

    public MainWindow_VM()
    {

        // register navigate message reciever
        WeakReferenceMessenger.Default.Register<NavigateToPage_Message>(this, (r, message) =>
        {
            SetCurrentPage(message.TargetPage);
        });

        SetCurrentPage("MainPage"); // set default page

        //currentUser = Services.AppData.Instance.CurrentUser;
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
