using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.Components;

public partial class PasswordInputField : UserControl
{

    public string ActualPassword
    {
        get { return (string)GetValue(ActualPasswordProperty); }
        set { SetValue(ActualPasswordProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ActualPassword.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ActualPasswordProperty =
        DependencyProperty.Register("ActualPassword", typeof(string), typeof(PasswordInputField), new PropertyMetadata(string.Empty));

    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register("Placeholder", typeof(string), typeof(PasswordInputField), new PropertyMetadata("Placeholder"));

    public PasswordInputField()
    {
        InitializeComponent();
    }

    private void PasswordInputFieldTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // password masking
        int currentLength = PasswordInputFieldTextBox.Text.Length;

        if (currentLength > ActualPassword.Length)
        {
            string newCharacters = PasswordInputFieldTextBox.Text.Substring(ActualPassword.Length);
            ActualPassword += newCharacters;
        }
        else if (currentLength < ActualPassword.Length)
        {
            ActualPassword = ActualPassword.Substring(0, currentLength);
        }

        PasswordInputFieldTextBox.Text = new string('●', ActualPassword.Length);
        PasswordInputFieldTextBox.CaretIndex = PasswordInputFieldTextBox.Text.Length;

        // palceholder visibility

        if (string.IsNullOrEmpty(PasswordInputFieldTextBox.Text))
        {
            PlaceholderLabel.Visibility = Visibility.Visible;
        }
        else
        {
            PlaceholderLabel.Visibility = Visibility.Collapsed;

        }
    }
}
