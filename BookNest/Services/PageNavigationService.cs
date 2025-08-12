using BookNest.Pages;
using BookNest.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace BookNest.Services;

public partial class PageNavigationService : ObservableObject
{
    private readonly IServiceProvider sp;

    [ObservableProperty]
    private Page currentPage;

    public static event Action PageNavigated;

    public PageNavigationService(IServiceProvider _sp)
    {
        sp = _sp;

        //SetCurrentPage("SignInPage"); // sets default page
    }

    public static void RaisePageNavigated()
    {
        PageNavigated?.Invoke();
    }

    // Page router
    public void SetCurrentPage(string targetPage)
    {
        try
        {
            if (targetPage == "SamplePage") CurrentPage = sp.GetRequiredService<SamplePage>();
            if (targetPage == "MainPage") CurrentPage = sp.GetRequiredService<MainPage>();
            if (targetPage == "SignInPage") CurrentPage = sp.GetRequiredService<SignInPage>();
            if (targetPage == "RegistrationPage") CurrentPage = sp.GetRequiredService<RegistrationPage>();

            RaisePageNavigated();
        }
        catch (Exception err)
        {
            Console.WriteLine("Page navigation FAILED");
            Console.Write(err.Message);
        }

    }
}
