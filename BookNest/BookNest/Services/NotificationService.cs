using BookNest.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookNest.Services;

public partial class NotificationService : ObservableObject
{
    private static NotificationService _instance;
    public static NotificationService Instance => _instance ??= new NotificationService();

    public int NotificationDuration = 5000; // toast duration in ms

    public ObservableCollection<NotificationItem> NotificationList { get; set; }

    public NotificationService()
    {
        NotificationList = new();
        //NotificationList.CollectionChanged += OnCollectionChanged;
    }

    // async add item
    async public void AddNotificationItem(NotificationToastStyle notificationType, string message)
    {

        NotificationList.Add(new NotificationItem(notificationType, message));

        await Task.Delay(NotificationDuration);

        Application.Current.Dispatcher.Invoke(() =>
        {
            NotificationList.Remove(NotificationList[0]);
        });
    }
}

// Notification item class
public class NotificationItem
{
    public string Message { get; set; } = string.Empty;
    public NotificationToastStyle Type { get; set; } = NotificationToastStyle.Default;

    public NotificationItem(NotificationToastStyle type, string message)
    {
        Type = type;
        Message = message;
    }
}
