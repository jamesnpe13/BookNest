using BookNest.ViewModels;
using System.Windows.Controls;

namespace BookNest.Pages;

public partial class MainPage : Page
{
    public MainPage(MainPage_VM _vm)
    {
        InitializeComponent();
        DataContext = _vm;
    }
}
