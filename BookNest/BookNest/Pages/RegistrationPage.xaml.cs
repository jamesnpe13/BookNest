using BookNest.Models;
using BookNest.Services;
using BookNest.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Pages;

public partial class RegistrationPage : Page
{
    private readonly RegistrationPage_VM vm;
    private readonly PageNavigationService ns;
    public string PasswordText { get; set; }
    public string ConfirmPasswordText { get; set; }
    public Account_M TempAccount { get; set; }

    private int stringLimitMax = 12;
    private int stringLimitMin = 2;
    private int UsernameStringLimitMin = 5;
    private int PasswordStringLimitMin = 5;

    public RegistrationPage(RegistrationPage_VM _vm, PageNavigationService _ns)
    {
        InitializeComponent();
        vm = _vm;
        ns = _ns;
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

    private void RegisterButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        PasswordText = PasswordField.ActualPassword;
        ConfirmPasswordText = ConfirmPasswordField.ActualPassword;

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
        SubmitForm();
    }

    private bool IsPasswordMatch()
    {
        return PasswordText == ConfirmPasswordText;
    }

    private void SubmitForm()
    {
        Console.WriteLine(PasswordText);
        Console.WriteLine(ConfirmPasswordText);
        Console.WriteLine(IsPasswordMatch());
        try
        {
            // check for empty fields
            if (string.IsNullOrEmpty(TempAccount.FirstName)) throw new Exception("First name field cannot be emtpy.");
            if (string.IsNullOrEmpty(TempAccount.LastName)) throw new Exception("Last name field cannot be emtpy.");
            if (string.IsNullOrEmpty(TempAccount.Username)) throw new Exception("Username field cannot be emtpy.");
            if (string.IsNullOrEmpty(TempAccount.Email)) throw new Exception("Email field cannot be emtpy.");
            if (string.IsNullOrEmpty(TempAccount.Password)) throw new Exception("Password field cannot be emtpy.");
            if (TempAccount.AccountType != "Member" && TempAccount.AccountType != "Administrator") throw new Exception("Please choose an account type.");

            // check if fields over max char or under min char
            if (TempAccount.FirstName.Count() < stringLimitMin || TempAccount.FirstName.Count() > stringLimitMax)
                throw new Exception($"First name must be between {stringLimitMin} and {stringLimitMax} characters.");
            if (TempAccount.LastName.Count() < stringLimitMin || TempAccount.LastName.Count() > stringLimitMax)
                throw new Exception($"Last name must be between {stringLimitMin} and {stringLimitMax} characters.");
            if (TempAccount.Username.Count() < UsernameStringLimitMin)
                throw new Exception($"Username must be greater than {UsernameStringLimitMin} characters.");
            if (TempAccount.Password.Count() < PasswordStringLimitMin)
                throw new Exception($"Password must be greater than {PasswordStringLimitMin} characters.");
            if (TempAccount.Email.Count() < 5)
                throw new Exception("Email address too short. Please enter a valid email address.");

            // check for white spaces
            if (TempAccount.Username.Any(c => char.IsWhiteSpace(c)))
                throw new Exception("Username cannot contain spaces.");
            if (TempAccount.Password.Any(c => char.IsWhiteSpace(c)))
                throw new Exception("Password cannot contain spaces.");

            // check password match
            if (!IsPasswordMatch()) throw new Exception("Passwords do not match.");

            vm.RegisterAccount(TempAccount, PasswordText);

        }
        catch (Exception err)
        {
            //throw new Exception(err.Message);
            NotificationService.Instance.AddNotificationItem(Components.NotificationToastStyle.Error, err.Message);
        }
    }

    private void CancelButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        ns.SetCurrentPage("SignInPage");
    }
}
