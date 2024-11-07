using BookNest.Pages;
using BookNest.Services;
using BookNest.ViewModels;
using BookNest.Views;
using System.Windows;

namespace BookNest;

public partial class MainWindow : Window
{
    public MainWindow(MainWindow_VM vm, PageNavigationService _ps)
    {
        InitializeComponent();
        MainFrame.DataContext = _ps;
        DataContext = vm;
    }
}