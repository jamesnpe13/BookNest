using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

namespace BookNest.Components
{
    public partial class Button : UserControl
    {
        // ******************************* BUTTON STYLE PROPERTY DEPENDENCY ****************************************

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
            DataContext = this;
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
                default:
                    UCLabel.Style = (Style)FindResource("ButtonLabelDefault");
                    UCBorder.Style = (Style)FindResource("ButtonBorderDefault");
                    break;
            }
        }
    }
}
