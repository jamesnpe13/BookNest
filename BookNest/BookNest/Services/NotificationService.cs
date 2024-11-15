using BookNest.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity.Core.Objects;
using System.Windows;

namespace BookNest.Services;

public partial class NotificationService : ObservableObject
{
    private static NotificationService _instance;
    public static NotificationService Instance => _instance ??= new NotificationService();

    public int NotificationDuration = 10000; // toast duration in ms

    public ObservableCollection<NotificationItem> NotificationList { get; set; }

    public NotificationService()
    {
        NotificationList = new();
        NotificationList.CollectionChanged += OnCollectionChanged;

        Console.WriteLine("NOTIF SERVICE");
    }

    // Trigger on notification item add
    public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        Console.WriteLine("Collection change");

        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var newItem in e.NewItems)
            {
                DissolveItem();
            }
        }
    }

    // async add item
    async public void AddNotificationItem(NotificationToastStyle notificationType, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            NotificationList.Add(new NotificationItem(notificationType, message));
            Console.WriteLine("NOTIF added");

        });
    }

    // async remove item after given duration
    async public void DissolveItem()
    {
        while (NotificationList.Count() > 0)
        {
            await Task.Delay(NotificationDuration);

            Application.Current.Dispatcher.Invoke(() =>
            {
                NotificationList.Remove(NotificationList[0]);
                Console.WriteLine("NOTIF Removed");
            });

        }
    }

    public void TestMessage()
    {
        Console.WriteLine("TEST MESSAGE");

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
