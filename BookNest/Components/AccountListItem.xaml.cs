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

namespace BookNest.Components;

public partial class AccountListItem : UserControl
{

    public string FirstName
    {
        get { return (string)GetValue(FirstNameProperty); }
        set { SetValue(FirstNameProperty, value); }
    }

    // Using a DependencyProperty as the backing store for FirstName.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FirstNameProperty =
        DependencyProperty.Register("FirstName", typeof(string), typeof(AccountListItem), new PropertyMetadata(string.Empty));

    public string LastName
    {
        get { return (string)GetValue(LastNameProperty); }
        set { SetValue(LastNameProperty, value); }
    }

    // Using a DependencyProperty as the backing store for LastName.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LastNameProperty =
        DependencyProperty.Register("LastName", typeof(string), typeof(AccountListItem), new PropertyMetadata(string.Empty));

    public string Username
    {
        get { return (string)GetValue(UsernameProperty); }
        set { SetValue(UsernameProperty, value); }
    }

    public string Email
    {
        get { return (string)GetValue(EmailProperty); }
        set { SetValue(EmailProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Email.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EmailProperty =
        DependencyProperty.Register("Email", typeof(string), typeof(AccountListItem), new PropertyMetadata(string.Empty));

    // Using a DependencyProperty as the backing store for Username.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UsernameProperty =
        DependencyProperty.Register("Username", typeof(string), typeof(AccountListItem), new PropertyMetadata(string.Empty));

    public string AccountType
    {
        get { return (string)GetValue(AccountTypeProperty); }
        set { SetValue(AccountTypeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for AccountType.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AccountTypeProperty =
        DependencyProperty.Register("AccountType", typeof(string), typeof(AccountListItem), new PropertyMetadata(string.Empty));

    public AccountListItem()
    {
        InitializeComponent();
    }
}
