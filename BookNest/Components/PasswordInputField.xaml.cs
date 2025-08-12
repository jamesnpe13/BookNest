using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.Components;

public partial class PasswordInputField : UserControl
{
    public FieldStyle FieldStyle
    {
        get { return (FieldStyle)GetValue(FieldStyleProperty); }
        set { SetValue(FieldStyleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for FieldStyle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FieldStyleProperty =
        DependencyProperty.Register("FieldStyle", typeof(FieldStyle), typeof(PasswordInputField), new PropertyMetadata(FieldStyle.Light, OnFieldStylePropertyChanged));

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

    private static void OnFieldStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PasswordInputField x = d as PasswordInputField;
        if (x != null)
        {
            FieldStyle newValue = (FieldStyle)e.NewValue;

            x.OnFieldStylePropertyChanged(newValue);
        }
    }

    protected virtual void OnFieldStylePropertyChanged(FieldStyle newValue)
    {
        if (newValue == FieldStyle.Dark)
        {
            TextInputFieldTextBox.Style = (Style)FindResource("TextBoxTextBox_Dark");
            TextInputBorder.Style = (Style)FindResource("TextBoxBorder_Dark");
            PlaceholderLabel.Style = (Style)FindResource("PlaceholderLabel_Dark");
        }
    }

    private void PasswordInputFieldTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // password masking
        int currentLength = TextInputFieldTextBox.Text.Length;

        if (currentLength > ActualPassword.Length)
        {
            string newCharacters = TextInputFieldTextBox.Text.Substring(ActualPassword.Length);
            ActualPassword += newCharacters;
        }
        else if (currentLength < ActualPassword.Length)
        {
            ActualPassword = ActualPassword.Substring(0, currentLength);
        }

        TextInputFieldTextBox.Text = new string('●', ActualPassword.Length);
        TextInputFieldTextBox.CaretIndex = TextInputFieldTextBox.Text.Length;

        // palceholder visibility

        if (string.IsNullOrEmpty(TextInputFieldTextBox.Text))
        {
            PlaceholderLabel.Visibility = Visibility.Visible;
        }
        else
        {
            PlaceholderLabel.Visibility = Visibility.Collapsed;

        }
    }
}
