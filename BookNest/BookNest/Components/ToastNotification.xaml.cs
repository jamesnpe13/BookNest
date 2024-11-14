﻿using System.Windows;
using System.Windows.Controls;

namespace BookNest.Components;
public enum NotificationToastStyle
{
    Null,
    Default,
    Success,
    Error,
    Warning
}
public partial class ToastNotification : UserControl
{

    public NotificationToastStyle ToastStyle
    {
        get { return (NotificationToastStyle)GetValue(ToastStyleProperty); }
        set { SetValue(ToastStyleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ToastStyle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ToastStyleProperty =
        DependencyProperty.Register("ToastStyle", typeof(NotificationToastStyle), typeof(ToastNotification), new PropertyMetadata(NotificationToastStyle.Null, OnToastStylePropertyChanged));

    public ToastNotification()
    {
        InitializeComponent();
    }

    // *** Property changed callback function
    private static void OnToastStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ToastNotification x = d as ToastNotification;
        if (x != null)
        {

            NotificationToastStyle newValue = (NotificationToastStyle)e.NewValue;

            x.OnToastStylePropertyChanged(newValue);
        }
    }

    protected virtual void OnToastStylePropertyChanged(NotificationToastStyle newValue)
    {
        switch (newValue)
        {
            case NotificationToastStyle.Warning:
                AccentBar.Style = (Style)FindResource("WarningToastStyle");
                break;
            case NotificationToastStyle.Success:
                AccentBar.Style = (Style)FindResource("SuccessToastStyle");
                break;
            case NotificationToastStyle.Error:
                AccentBar.Style = (Style)FindResource("ErrorToastStyle");
                break;
            case NotificationToastStyle.Default:
                AccentBar.Style = (Style)FindResource("DefaultToastStyle");
                break;
        }
    }
}
