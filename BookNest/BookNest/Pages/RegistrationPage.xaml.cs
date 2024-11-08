using BookNest.ViewModels;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Pages;

public partial class RegistrationPage : Page
{
    private readonly RegistrationPage_VM vm;

    public RegistrationPage(RegistrationPage_VM _vm)
    {
        InitializeComponent();
        vm = _vm;
        DataContext = vm;
    }

    private void SubmitIfNotEmptyOrNull(string field)
    {
        if (!string.IsNullOrEmpty(field)) SubmitForm();
    }

    private void UsernameField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            SubmitIfNotEmptyOrNull(UsernameField.TextInputFieldTextBox.Text);
        }
    }

    private void PasswordField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            SubmitIfNotEmptyOrNull(UsernameField.TextInputFieldTextBox.Text);
        }
    }

    private void RegisterButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SubmitForm();
    }

    private void SubmitForm()
    {
        vm.SubmitForm();
    }

}
