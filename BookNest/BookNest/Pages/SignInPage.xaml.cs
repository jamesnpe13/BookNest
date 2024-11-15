﻿using BookNest.Components;
using BookNest.Services;
using BookNest.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookNest.Pages;

public partial class SignInPage : Page
{
    private readonly SignInPage_VM vm;
    private readonly AppData ad;
    private readonly PageNavigationService pn;

    public SignInPage(SignInPage_VM _vm, AppData _ad, PageNavigationService _pn)
    {
        InitializeComponent();
        vm = _vm;
        ad = _ad;
        pn = _pn;
        DataContext = vm;
        //ns = _ns;
    }

    private void UsernameField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Console.WriteLine("Enter is pressed");
            if (!string.IsNullOrEmpty(UsernameField.TextInputFieldTextBox.Text))
            {
                SubmitForm();

            }
        }
    }

    private void PasswordField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (!string.IsNullOrEmpty(PasswordField.PasswordInputFieldTextBox.Text))
            {
                SubmitForm();
            }
        }
    }

    private void PasswordField_LostFocus(object sender, System.Windows.RoutedEventArgs e)
    {

    }

    private void SignInButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SubmitForm();
    }

    private void SubmitForm()
    {
        vm.SubmitForm(PasswordField.ActualPassword);
    }

    private void SwitchTypeButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        vm.IsAdmin = !vm.IsAdmin;
        vm.SetFormType();
        Console.WriteLine("IsAdmin: " + vm.IsAdmin);
    }

    private void CreateAccountButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        pn.SetCurrentPage("RegistrationPage");
    }

    private void ResetPasswordButton_MouseDown(object sender, MouseButtonEventArgs e)
    {

    }

    private void AddNotifButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        NotificationService.Instance.AddNotificationItem(NotificationToastStyle.Success, "Great! The notification service is now working.");
    }

    private void AddNotifButton2_MouseDown(object sender, MouseButtonEventArgs e)
    {
        NotificationService.Instance.AddNotificationItem(NotificationToastStyle.Warning, "Great! The notification service is now working.");
    }

    private void AddNotifButton3_MouseDown(object sender, MouseButtonEventArgs e)
    {
        NotificationService.Instance.AddNotificationItem(NotificationToastStyle.Default, "Great! The notification service is now working.");
    }

    private void AddNotifButton4_MouseDown(object sender, MouseButtonEventArgs e)
    {
        NotificationService.Instance.AddNotificationItem(NotificationToastStyle.Error, "Great! The notification service is now working.");
    }
}
