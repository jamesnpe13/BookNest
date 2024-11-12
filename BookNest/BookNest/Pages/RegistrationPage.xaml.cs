using BookNest.Models;
using BookNest.Services;
using BookNest.ViewModels;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Pages;

public partial class RegistrationPage : Page
{

    private readonly RegistrationPage_VM vm;
    public string PasswordText { get; set; }
    public string ConfirmPasswordText { get; set; }
    public bool IsPasswordMatch { get; set; } = false;
    public Account_M TempAccount { get; set; }

    public RegistrationPage(RegistrationPage_VM _vm)
    {
        InitializeComponent();
        vm = _vm;
        DataContext = vm;
        CreateComboboxItems();

        TempAccount = new();

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

    // ------------- form input event handlers

    private void PasswordField_LostFocus(object sender, System.Windows.RoutedEventArgs e)
    {
        PasswordText = PasswordField.ActualPassword;

        // check if p2 is null or empty
        // if not null or empty then check match

        if (!string.IsNullOrEmpty(ConfirmPasswordText))
        {
            CheckPasswordMatch();
        }

    }

    private void ConfirmPasswordField_LostFocus(object sender, System.Windows.RoutedEventArgs e)
    {
        ConfirmPasswordText = ConfirmPasswordField.ActualPassword;

        CheckPasswordMatch();
    }

    private void CheckPasswordMatch()
    {
        //if (string.IsNullOrEmpty(p1)) ;
        IsPasswordMatch = PasswordText == ConfirmPasswordText ? true : false;
        Console.WriteLine("IsPasswordMatch: " + IsPasswordMatch);
    }

    private void RegisterButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

        // populate tempAccount
        TempAccount.FirstName = FirstNameField.TextInputFieldTextBox.Text;
        TempAccount.LastName = LastNameField.TextInputFieldTextBox.Text;
        TempAccount.Username = UsernameField.TextInputFieldTextBox.Text;
        TempAccount.Email = EmailField.TextInputFieldTextBox.Text;
        TempAccount.Password = PasswordText;

        if (RegistrationTypeDropdown.DropdownCombobox.SelectedItem is ComboBoxItem selectedItem)
        {
            string selectedValue = selectedItem.Content.ToString();
            TempAccount.AccountType = selectedValue;
        }

        // validate and submit
        if (IsFormValid()) SubmitForm();
    }

    private bool IsFormValid()
    {
        if (IsPasswordMatch)
        {
            Console.WriteLine("Form validation passed.");
            return true;
        }

        Console.WriteLine("Form validation failed.");
        return false;
    }

    private void SubmitForm()
    {
        vm.RegisterAccount(TempAccount, PasswordText);
        vm.SubmitForm();
    }

}
