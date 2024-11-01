using BookNest.Services;
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

        private void SignInPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage("SignInPage");
        }

        private void RegistrationPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage("RegistrationPage");
        }

        private void MainPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage("MainPage");
        }

        public void NavigateToPage(string targetPage)
        {
            WeakReferenceMessenger.Default.Send(new NavigateToPage_Message(targetPage));
        }
    }
}
