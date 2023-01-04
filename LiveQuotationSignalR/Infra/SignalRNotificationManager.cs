using LiveQuotationSignalR.Domain;
using LiveQuotationSignalR.Infra.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LiveQuotationSignalR.Infra
{
    public class SignalRNotificationManager : INotificationManager
    {
        private readonly IHubContext<QuotationHub> _hubContext;

        public SignalRNotificationManager(IHubContext<QuotationHub> hubContext)
        {
            _hubContext = hubContext;
        }


        public async Task NotifyGroup<T>(string groupName, string methodName, T message, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(methodName, message, cancellationToken);
        }
    }
}
