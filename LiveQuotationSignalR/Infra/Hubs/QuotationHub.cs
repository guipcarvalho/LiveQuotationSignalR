using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace LiveQuotationSignalR.Infra.Hubs;

[SignalRHub("/quotationHub")]
public class QuotationHub : Hub
{
    public Task SubscribeAssetQuotation(string assetTicker, CancellationToken cancellationToken) => Groups.AddToGroupAsync(Context.ConnectionId, assetTicker, cancellationToken);

    public Task UnsubscribeAssetQuotation(string assetTicker, CancellationToken cancellationToken) => Groups.RemoveFromGroupAsync(Context.ConnectionId, assetTicker, cancellationToken);
}