using BookNest.Pages;
using BookNest.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;

namespace BookNest.ViewModels;

partial class MainWindow_VM : ObservableObject
{
    // pages as objects
    private readonly object _MainPage = new MainPage();
    private readonly object _RegistrationPage = new RegistrationPage();
    private readonly object _SignInPage = new SignInPage();

    [ObservableProperty] private string statusMessage;
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

    [RelayCommand]
    public void SetCurrentPage(string page)
    {
        if (page == "MainPage") CurrentPage = _MainPage;
        if (page == "SignInPage") CurrentPage = _SignInPage;
        if (page == "RegistrationPage") CurrentPage = _RegistrationPage;
    }

    // Override OnPropertyChanged to handle property changes
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        // Check if the property name matches the one you want to handle
        if (e.PropertyName == nameof(TargetPage)) SetCurrentPage(TargetPage); // override TargetPage property
    }
}
