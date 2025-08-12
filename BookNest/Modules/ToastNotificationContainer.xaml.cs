using BookNest.Services;
using BookNest.ViewModels;
using System.Windows.Controls;

namespace BookNest.Modules;

public partial class ToastNotificationContainer : UserControl
{
    public ToastNotificationContainer()
    {
        InitializeComponent();
        DataContext = NotificationService.Instance;
    }

}
