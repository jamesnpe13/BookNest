﻿using BookNest.ViewModels;
using System.Windows.Controls;

namespace BookNest.Views;

public partial class Admin_Dashboard_V : UserControl
{
    public Admin_Dashboard_V()
    {
        InitializeComponent();
    }

    private void ManageBooksButton_MouseDown() => SwitchView(PageView.Books);
    private void ManageReturnsButton_MouseDown() => SwitchView(PageView.Returns);
    private void ManageAccountsButton_MouseDown() => SwitchView(PageView.Account);
    private void QuickAddBookButton_MouseDown()
    {
        //
    }

    private void SwitchView(PageView targetView)
    {
        if (DataContext is MainPage_VM vm) vm.SetCurrentView(targetView);
    }
}
