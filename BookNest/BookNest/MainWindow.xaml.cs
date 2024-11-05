using BookNest.Pages;
using BookNest.ViewModels;
using BookNest.Views;
using System.Windows;

namespace BookNest;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindow_VM();

        MainFrame.Navigate(new MainPage());

    }
}