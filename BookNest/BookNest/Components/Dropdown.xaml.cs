using System.Windows;
using System.Windows.Controls;

namespace BookNest.Components
{
    public partial class Dropdown : UserControl
    {
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(Dropdown), new PropertyMetadata("Placeholder"));

        public Dropdown()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
