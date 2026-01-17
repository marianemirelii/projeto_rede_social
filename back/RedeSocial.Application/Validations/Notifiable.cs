namespace RedeSocial.Application.Validations;

public class Notifiable
{
    private readonly List<Notification> _notifications = new();

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public void AddNotification(Notification notification) => _notifications.Add(notification);

    public void AddNotifications(IEnumerable<Notification> notifications) => _notifications.AddRange(notifications);

    public bool IsValid => !_notifications.Any();

    protected void AddNotification(string message)
    {
        _notifications.Add(new Notification(message));
    }

}
