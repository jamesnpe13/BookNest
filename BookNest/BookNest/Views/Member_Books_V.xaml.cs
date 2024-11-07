using BookNest.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace BookNest.Views;

public partial class Member_Books_V : UserControl
{

    public Member_Books_V()
    {
        InitializeComponent();
        //BooksList.DataContext = DataCollections.Instance;
    }
}
