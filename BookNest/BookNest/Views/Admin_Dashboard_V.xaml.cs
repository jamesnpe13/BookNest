﻿using BookNest.Data;
using BookNest.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Views;

public partial class Admin_Dashboard_V : UserControl
{
    private readonly DatabaseService ds;
    public Admin_Dashboard_V()
    {
        InitializeComponent();
        ds = ((App)Application.Current).ServiceProvider.GetRequiredService<DatabaseService>();
    }

    private void ManageBooksButton_MouseDown(object sender, MouseButtonEventArgs e) => SetCurrentView(PageView.Books);
    private void ManageReturnsButton_MouseDown(object sender, MouseButtonEventArgs e) => SetCurrentView(PageView.Returns);
    private void ManageAccountsButton_MouseDown(object sender, MouseButtonEventArgs e) => SetCurrentView(PageView.Account);
    private void QuickAddBookButton_MouseDown(object sender, MouseButtonEventArgs e) => SetCurrentView(PageView.BookAdd);

    private void SetCurrentView(PageView targetView)
    {
        if (DataContext is MainPage_VM vm) vm.SetCurrentView(targetView);
    }

    private void DashboardSearchField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            SearchBooks();
        }
    }

    private void DashboardSearchButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SearchBooks();
    }

    public void SearchBooks()
    {
        if (!string.IsNullOrEmpty(DashboardSearchField.Text))
        {
            if (DataContext is MainPage_VM vm)
            {
                vm.SetCurrentView(PageView.Books);
                vm.TempBookList = new();
                vm.TempBookList = ds.GetBook(BookFilterKey.SEARCH, DashboardSearchField.Text);
                //vm.UpdateBookList(BookFilterKey.SEARCH, DashboardSearchField.Text);
                vm.RefreshBookList();
                DashboardSearchField.Text = string.Empty;
            }
        }
    }
}
