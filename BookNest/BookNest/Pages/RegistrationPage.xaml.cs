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
        CreateComboboxItems();

    }

    private void CreateComboboxItems()
    {
        //ComboBoxItem cbItem1 = new ComboBoxItem();
        ComboBoxItem cbItem2 = new ComboBoxItem();
        ComboBoxItem cbItem3 = new ComboBoxItem();

        //cbItem1.Content = "Account Type";
        cbItem2.Content = "Member";
        cbItem3.Content = "Administrator";

        //RegistrationTypeDropdown.DropdownCombobox.Items.Add(cbItem1);
        RegistrationTypeDropdown.DropdownCombobox.Items.Add(cbItem2);
        RegistrationTypeDropdown.DropdownCombobox.Items.Add(cbItem3);
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

    private void ConfirmPasswordField_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void FirstNameField_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void LastNameField_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void EmailField_KeyDown(object sender, KeyEventArgs e)
    {

    }
}
