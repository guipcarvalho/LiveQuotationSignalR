namespace LiveQuotationSignalR.Domain
{
    public interface INotificationManager
    {
        Task NotifyGroup<T>(string groupName, string methodName, T message, CancellationToken cancellationToken);
    }
}
