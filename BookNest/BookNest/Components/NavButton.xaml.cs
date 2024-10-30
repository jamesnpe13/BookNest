using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    public partial class NavButton : UserControl
    {

        // ****************** Button style dependency property and CLR

        public string ButtonStyle
        {
            get { return (string)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register("ButtonStyle", typeof(string), typeof(NavButton), new PropertyMetadata(null, OnButtonStylePropertyChanged));

        private static void OnButtonStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var x = d as NavButton;
            string newValue = (string)e.NewValue;
            x.OnButtonStylePropertyChanged(newValue);
        }

        protected virtual void OnButtonStylePropertyChanged(string newValue)
        {
            switch (ButtonStyle)
            {
                case "Primary":
                    NavButtonLabel.Style = (Style)FindResource("ButtonLabelPrimary");
                    NavButtonBorder.Style = (Style)FindResource("ButtonBorderPrimary");
                    break;
                case "Secondary":
                    NavButtonLabel.Style = (Style)FindResource("ButtonLabelSecondary");
                    NavButtonBorder.Style = (Style)FindResource("ButtonBorderSecondary");
                    break;
                case "Destructive":
                    NavButtonLabel.Style = (Style)FindResource("ButtonLabelDestructive");
                    NavButtonBorder.Style = (Style)FindResource("ButtonBorderDestructive");
                    break;
                case "Transparent":
                    NavButtonLabel.Style = (Style)FindResource("ButtonLabelTransparent");
                    NavButtonBorder.Style = (Style)FindResource("ButtonBorderTransparent");
                    break;
                default:
                    NavButtonLabel.Style = (Style)FindResource("ButtonLabelDefault");
                    NavButtonBorder.Style = (Style)FindResource("ButtonBorderDefault");
                    break;
            }
        }

        // ****************** Button text dependency property and CLR

        public string ButtonText
        {

            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(NavButton), new PropertyMetadata("No button text"));

        // ****************** Icon source dependency property and CLR

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(NavButton), new PropertyMetadata(null));

        // ****************** Constructor

        public NavButton()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
