using BookNest.ViewModels;
using System.Windows.Controls;

namespace BookNest.Pages;

public partial class MainPage : Page
{
    private readonly MainPage_VM vm;
    public MainPage(MainPage_VM _vm)
    {
        InitializeComponent();
        vm = _vm;
        DataContext = vm;
    }
}
