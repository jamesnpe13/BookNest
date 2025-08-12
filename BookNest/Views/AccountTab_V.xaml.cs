using BookNest.Models;
using BookNest.Services;
using Microsoft.Extensions.DependencyInjection;
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

namespace BookNest.Views;

public partial class AccountTab_V : UserControl
{
    private readonly AppData ad;

    public Account_M CurrentAccount { get; set; }

    public AccountTab_V()
    {
        InitializeComponent();
        DataContext = this;
        ad = ((App)Application.Current).ServiceProvider.GetRequiredService<AppData>();
        CurrentAccount = ad.CurrentAccount;
    }
}
