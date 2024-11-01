using BookNest.ViewModels;
using System.Windows;

namespace BookNest;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainWindow_VM vm = new();
        DataContext = vm;
    }
}