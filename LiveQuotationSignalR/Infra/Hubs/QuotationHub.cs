using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;

namespace LiveQuotationSignalR.Infra.Hubs;

public class QuotationHub : Hub
{
    public async Task SubscribeAssetQuotation(string assetTicker, CancellationToken cancellationToken)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, assetTicker, cancellationToken);
    }

    public async Task UnsubscribeAssetQuotation(string assetTicker, CancellationToken cancellationToken)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, assetTicker, cancellationToken);
    }
}