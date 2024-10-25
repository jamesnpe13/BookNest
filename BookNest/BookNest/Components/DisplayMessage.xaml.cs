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
    /// <summary>
    /// Interaction logic for DisplayMessage.xaml
    /// </summary>
    public partial class DisplayMessage : UserControl
    {
        private string mainText;
        private string subText;

        public string MainText
        {
            get { return "BookNest"; }
            set { mainText = value; }
        }

        public string SubText
        {
            get { return "Library Management System"; }
            set { mainText = value; }
        }

        public DisplayMessage()
        {
            DataContext = this;
            InitializeComponent();
        }
    }
}
