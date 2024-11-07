using BookNest.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Modules
{
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
