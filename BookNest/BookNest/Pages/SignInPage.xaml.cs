using BookNest.Services;
using BookNest.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Pages;

public partial class SignInPage : Page
{
    private readonly SignInPage_VM vm;
    public SignInPage(SignInPage_VM _vm)
    {
        InitializeComponent();
        vm = _vm;
        DataContext = vm;
        //ns = _ns;
    }

    private void UsernameField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (!string.IsNullOrEmpty(UsernameField.TextInputFieldTextBox.Text))
            {
                SubmitForm();
            }
        }
    }

    private void PasswordField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (!string.IsNullOrEmpty(PasswordField.PasswordInputFieldTextBox.Password))
            {
                SubmitForm();
            }
        }
    }

    private void SignInButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SubmitForm();
    }

    private void SubmitForm()
    {
        vm.SubmitForm();
    }

}
