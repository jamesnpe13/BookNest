using BookNest.Data;
using BookNest.Models;
using BookNest.ViewModels;
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

public partial class Member_Account_V : UserControl
{
    private readonly MainPage_VM vm;
    private readonly DatabaseService ds;
    private readonly MemberAccount_VM mavm;

    public Member_Account_V()
    {
        InitializeComponent();

        vm = ((App)Application.Current).ServiceProvider.GetRequiredService<MainPage_VM>();
        ds = ((App)Application.Current).ServiceProvider.GetRequiredService<DatabaseService>();
        mavm = ((App)Application.Current).ServiceProvider.GetRequiredService<MemberAccount_VM>();

        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<MemberAccount_VM>();

    }

    private void backButtonUtility_MouseDown(object sender, MouseButtonEventArgs e)
    {
        vm.NavigateBack();
    }

    private void AddBookButtonUtility_MouseDown(object sender, MouseButtonEventArgs e)
    {
        vm.SetCurrentView(PageView.BookAdd);
    }

    private void AccountTabButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        mavm.SetCurentView("Account");
    }

    private void LoanedBooksTabButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        mavm.SetCurentView("LoanedBooks");

    }

    private void HistoryTabButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        mavm.SetCurentView("History");

    }

    private void ReservedTabButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        mavm.SetCurentView("Reserved");
    }
}
