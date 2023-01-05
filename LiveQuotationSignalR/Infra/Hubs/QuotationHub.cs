using LiveQuotationSignalR.Gateways;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace LiveQuotationSignalR.Infra.Hubs;

[SignalRHub("/quotationHub")]
public class QuotationHub : Hub
{
    private readonly IQuotationGateway _gateway;

    public QuotationHub(IQuotationGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<string?> SubscribeAssetQuotation(string assetTicker)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, assetTicker);

        var quotation = await _gateway.GetAssetQuotationAsync(assetTicker, CancellationToken.None);

        return quotation?.UsdBrl?.Ask;
    }

    public Task UnsubscribeAssetQuotation(string assetTicker) => Groups.RemoveFromGroupAsync(Context.ConnectionId, assetTicker);
}