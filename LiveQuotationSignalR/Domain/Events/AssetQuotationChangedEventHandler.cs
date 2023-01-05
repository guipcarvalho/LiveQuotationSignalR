using MediatR;

namespace LiveQuotationSignalR.Domain.Events
{
    public class AssetQuotationChangedEventHandler : INotificationHandler<AssetQuotationChangedEvent>
    {
        private readonly INotificationManager _notificationManager;

        public AssetQuotationChangedEventHandler(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        public Task Handle(AssetQuotationChangedEvent notification, CancellationToken cancellationToken)
        {
            if(notification.AssetTicker is null)
                throw new ArgumentNullException(nameof(notification.AssetTicker));

            return _notificationManager.NotifyGroup(notification.AssetTicker, "AssetQuotation", notification.Quotation, cancellationToken);
        }
    }
}
