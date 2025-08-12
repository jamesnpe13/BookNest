using System;
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
    public partial class TextButton : UserControl
    {
        public TextButton()
        {
            InitializeComponent();
        }

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(TextButton), new PropertyMetadata("Not bound"));

        public string ButtonStyle
        {
            get { return (string)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonStyleProperty =
             DependencyProperty.Register("ButtonStyle", typeof(string), typeof(TextButton), new PropertyMetadata("White", OnButtonStylePropertyChanged));

        private static void OnButtonStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextButton x = d as TextButton;
            if (x != null)
            {
                string newValue = (string)e.NewValue;

                x.OnButtonStylePropertyChanged(newValue);
            }
        }

        protected virtual void OnButtonStylePropertyChanged(string newValue)
        {
            switch (newValue)
            {
                case "Primary":
                    ButtonTextBlock.Foreground = (Brush)FindResource("Primary");
                    break;
                case "Secondary":
                    ButtonTextBlock.Foreground = (Brush)FindResource("Blue2");
                    break;
                case "Destructive":
                    ButtonTextBlock.Foreground = (Brush)FindResource("Destructive");
                    break;
                default:
                    ButtonTextBlock.Foreground = (Brush)FindResource("White");
                    break;
            }
        }

    }
}
