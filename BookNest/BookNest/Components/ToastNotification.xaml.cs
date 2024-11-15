using BookNest.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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

    public string Message
    {
        get { return (string)GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register("Message", typeof(string), typeof(ToastNotification), new PropertyMetadata(string.Empty));

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

        FadeIn();
        FadeOut();
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

    private void FadeIn()
    {
        var fadeInStoryboard = (Storyboard)Resources["FadeInStoryboard"];
        fadeInStoryboard.Begin(this);
    }

    async private void FadeOut()
    {
        Console.WriteLine("before fade");
        await Task.Delay(NotificationService.Instance.NotificationDuration - 500);

        Application.Current.Dispatcher.Invoke(() =>
        {
            Console.WriteLine("before fade");
            var fadeOutStoryboard = (Storyboard)Resources["FadeOutStoryboard"];
            fadeOutStoryboard.Begin(this);
        });
    }

}
