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

namespace BookNest.Components
{
    public partial class NavButton : UserControl
    {
        // button style property
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register(
                nameof(ButtonStyle),
                typeof(Style),
                typeof(NavButton),
                new PropertyMetadata(null));

        public Style ButtonStyle
        {
            get => (Style)GetValue(ButtonStyleProperty);
            set => SetValue(ButtonStyleProperty, value);
        }

        // button text property
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(
                nameof(ButtonText),
                typeof(Object),
                typeof(NavButton),
                new PropertyMetadata("Nav Button"));

        public Object ButtonText
        {
            get => GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public NavButton()
        {
            InitializeComponent();
        }
    }
}
