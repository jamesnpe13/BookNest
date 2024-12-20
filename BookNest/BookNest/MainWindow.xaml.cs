﻿using BookNest.Pages;
using BookNest.Services;
using BookNest.ViewModels;
using BookNest.Views;
using System.Windows;

namespace BookNest;

public partial class MainWindow : Window
{
    private readonly SessionService ss;

    public MainWindow(MainWindow_VM vm, PageNavigationService _ps, SessionService _ss)
    {
        InitializeComponent();
        ss = _ss;
        MainFrame.DataContext = _ps;
        DataContext = vm;
    }

    private void MainWin_Loaded(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("Main window loaded");

        //Console.WriteLine("Auto signin user on window load (MainWindow.xaml.cs)");
        //ss.HandleUserSignIn("member", "123", "Member");
    }
}