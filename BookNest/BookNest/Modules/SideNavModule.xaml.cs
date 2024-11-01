using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
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
using BookNest.Services;
using BookNest.Pages;

namespace BookNest.Modules
{
    public partial class SideNavModule : UserControl
    {
        public SideNavModule()
        {
            InitializeComponent();
        }

        // method for sending message to navigate between pages
        public void NavigateToPage(string page)
        {
            WeakReferenceMessenger.Default.Send(new NavigateToPage_Message(page));
        }

        // navigate to page on click
        private void SignOutButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage("SignInPage");
        }
    }
}
