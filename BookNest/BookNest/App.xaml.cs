using BookNest.ViewModels;
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
        // register services here
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindow_VM>();

    }
}
