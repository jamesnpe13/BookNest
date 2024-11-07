using BookNest.Pages;
using BookNest.ViewModels;
using BookNest.Views;
using System.Windows;

namespace BookNest;

public partial class MainWindow : Window
{
    public MainWindow(MainWindow_VM vm)
    {
        InitializeComponent();
        DataContext = vm;

        MainFrame.Navigate(new MainPage());

    }
}