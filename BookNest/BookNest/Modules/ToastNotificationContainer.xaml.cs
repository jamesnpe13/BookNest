using BookNest.Components;
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

namespace BookNest.Modules;

public class NotificationItem
{
    public NotificationToastStyle NotificationType { get; set; }
    public string NotificationMessage { get; set; } = string.Empty;
}

public partial class ToastNotificationContainer : UserControl
{
    public List<NotificationItem> NotificationList { get; set; }

    public ToastNotificationContainer()
    {
        InitializeComponent();
        DataContext = this;

        NotificationList = new();

        NotificationItem nItem1 = new() { NotificationType = NotificationToastStyle.Default, NotificationMessage = "This is a notification 1" };
        NotificationItem nItem2 = new() { NotificationType = NotificationToastStyle.Error, NotificationMessage = "This is a notification 2" };

        NotificationList.Add(nItem1);
        NotificationList.Add(nItem2);
    }
}
