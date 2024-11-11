using CommunityToolkit.Mvvm.ComponentModel;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.Components;

public partial class Button : UserControl
{
    // ******************************* BUTTON STYLE PROPERTY DEPENDENCY ****************************************

    public string ButtonHeight
    {
        get { return (string)GetValue(ButtonHeightProperty); }
        set { SetValue(ButtonHeightProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ButtonHeight.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ButtonHeightProperty =
        DependencyProperty.Register("ButtonHeight", typeof(string), typeof(Button), new PropertyMetadata(null, OnButtonHeightPropertyChanged));

    // *** CLR Wrapper
    public string ButtonStyle
    {
        get { return (string)GetValue(ButtonStyleProperty); }
        set { SetValue(ButtonStyleProperty, value); }
    }
    // *** REGISTER DEPENDENCY
    public static readonly DependencyProperty ButtonStyleProperty =
        DependencyProperty.Register("ButtonStyle", typeof(string), typeof(Button), new PropertyMetadata(null, OnButtonStylePropertyChanged));

    // ******************************* BUTTON TEXT PROPERTY DEPENDENCY ****************************************
    public string ButtonText
    {
        get { return (string)GetValue(ButtonTextProperty); }
        set { SetValue(ButtonTextProperty, value); }
    }
    public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register("ButtonText", typeof(string), typeof(Button), new PropertyMetadata("No button text"));

    // ******************************* CONSTRUCTOR ****************************************
    // 
    public Button()
    {
        InitializeComponent();
    }

    // *** Property changed callback function
    private static void OnButtonStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Button x = d as Button;
        if (x != null)
        {
            string oldValue = (string)e.OldValue;
            string newValue = (string)e.NewValue;

            x.OnButtonStylePropertyChanged(oldValue, newValue);
        }
    }

    private static void OnButtonHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Button x = d as Button;
        if (x != null)
        {
            string newValue = (string)e.NewValue;

            x.OnButtonHeightPropertyChanged(newValue);
        }
    }

    protected virtual void OnButtonHeightPropertyChanged(string newValue)
    {
        if (newValue == "Tall") this.Height = 50;
        if (newValue == "Medium") this.Height = 40;
    }

    // *** Instance method
    protected virtual void OnButtonStylePropertyChanged(string oldValue, string newValue)
    {
        switch (newValue)
        {
            case "Primary":
                UCLabel.Style = (Style)FindResource("ButtonLabelPrimary");
                UCBorder.Style = (Style)FindResource("ButtonBorderPrimary");
                break;
            case "Secondary":
                UCLabel.Style = (Style)FindResource("ButtonLabelSecondary");
                UCBorder.Style = (Style)FindResource("ButtonBorderSecondary");
                break;
            case "Destructive":
                UCLabel.Style = (Style)FindResource("ButtonLabelDestructive");
                UCBorder.Style = (Style)FindResource("ButtonBorderDestructive");
                break;
            case "Transparent":
                UCLabel.Style = (Style)FindResource("ButtonLabelTransparent");
                UCBorder.Style = (Style)FindResource("ButtonBorderTransparent");
                break;
            case "Neutral":
                UCLabel.Style = (Style)FindResource("ButtonLabelNeutral");
                UCBorder.Style = (Style)FindResource("ButtonBorderNeutral");
                break;
            default:
                UCLabel.Style = (Style)FindResource("ButtonLabelDefault");
                UCBorder.Style = (Style)FindResource("ButtonBorderDefault");
                break;
        }
    }
}
