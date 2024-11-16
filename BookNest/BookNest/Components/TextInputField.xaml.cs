using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public enum FieldStyle
{
    Dark,
    Light
}

namespace BookNest.Components
{
    public partial class TextInputField : UserControl
    {

        public FieldStyle FieldStyle
        {
            get { return (FieldStyle)GetValue(FieldStyleProperty); }
            set { SetValue(FieldStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FieldStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FieldStyleProperty =
            DependencyProperty.Register("FieldStyle", typeof(FieldStyle), typeof(TextInputField), new PropertyMetadata(FieldStyle.Light, OnFieldStylePropertyChanged));

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPasswordProperty =
            DependencyProperty.Register("IsPassword", typeof(bool), typeof(TextInputField), new PropertyMetadata(false));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextInputField), new PropertyMetadata(null));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TextInputField), new PropertyMetadata("Placeholder"));

        public TextInputField()
        {
            InitializeComponent();
        }

        private static void OnFieldStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextInputField x = d as TextInputField;
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

        private void TextInputFieldTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
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
}
