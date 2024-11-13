using BookNest.Data;
using BookNest.Pages;
using BookNest.Services;
using BookNest.ViewModels;
using BookNest.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using System.Windows;

namespace BookNest;

public partial class App : Application
{
    // enable console for debuggin
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    public App()
    {
        InitializeComponent(); // initialize to ensure static resource is loaded first

        AllocConsole(); // enables console

        ServiceCollection serviceCollection = new();
        serviceCollection.ConfigureServices();

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        // instantiate MainWindow
        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

}

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        // services
        services.AddSingleton<PasswordManager>();
        services.AddSingleton<AppData>();
        services.AddSingleton<PageNavigationService>();
        services.AddSingleton<SessionService>();

        // database
        services.AddSingleton<DatabaseService>();

        // main window
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindow_VM>();

        services.AddTransient<SamplePage>();

        // pages
        services.AddTransient<MainPage>();
        services.AddTransient<MainPage_VM>();

        services.AddTransient<SignInPage>();
        services.AddTransient<SignInPage_VM>();

        services.AddTransient<RegistrationPage>();
        services.AddTransient<RegistrationPage_VM>();

        // page views
        services.AddTransient<Admin_Dashboard_V>();
        services.AddTransient<Member_Dashboard_V>();

        services.AddTransient<Admin_Account_V>();
        services.AddTransient<Member_Account_V>();

        services.AddTransient<Admin_Books_V>();
        services.AddTransient<Member_Books_V>();

        services.AddTransient<Admin_People_V>();
        services.AddTransient<Admin_Reserved_V>();
        services.AddTransient<Admin_Returns_V>();

        services.AddTransient<Member_Bag_V>();
        services.AddTransient<Member_Watchlist_V>();

        services.AddTransient<Book_Details_V>();
        services.AddTransient<Book_AddUpdate_V>();
    }
}
