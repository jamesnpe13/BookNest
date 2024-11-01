using BookNest.ViewModels;
using System.Windows.Controls;

namespace BookNest.Pages;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        MainPage_VM vm = new();
        DataContext = vm;
    }
}
