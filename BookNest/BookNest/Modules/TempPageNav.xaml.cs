using BookNest.Services;
using BookNest.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
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

namespace BookNest.Modules
{
    /// <summary>
    /// Interaction logic for TempPageNav.xaml
    /// </summary>
    public partial class TempPageNav : UserControl
    {

        public TempPageNav()
        {
            InitializeComponent();
        }

        // page navigation router
        private void SignInPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainWindow_VM vm)
                vm.SetCurrentPage("SignInPage");
        }

        private void RegistrationPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainWindow_VM vm)
                vm.SetCurrentPage("RegistrationPage");
        }
        private void MainPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainWindow_VM vm)
                vm.SetCurrentPage("MainPage");
        }
    }
}
