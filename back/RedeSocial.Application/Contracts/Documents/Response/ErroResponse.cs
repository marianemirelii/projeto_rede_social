using RedeSocial.Application.Validations;

namespace RedeSocial.Application.Contracts.Documents.Response;

public class ErroResponse
{
    public IReadOnlyCollection<Notification> Notifications { get; set; }

    public ErroResponse(IReadOnlyCollection<Notification> notifications)
    {
        Notifications = notifications;
    }

    public ErroResponse(Notification notification)
    {
        Notifications = new List<Notification> { notification };
    }

}
