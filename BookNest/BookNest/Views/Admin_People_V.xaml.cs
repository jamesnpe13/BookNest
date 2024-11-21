using BookNest.Models;
using BookNest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookNest.Views;

public partial class Admin_People_V : UserControl
{
    public Admin_People_V()
    {
        InitializeComponent();
    }
    private void backButtonUtility_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.NavigateBack();
        }
    }

    private void AddBookButtonUtility_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainPage_VM vm)
        {
            vm.SetCurrentView(PageView.BookAdd);
        }
    }
}
