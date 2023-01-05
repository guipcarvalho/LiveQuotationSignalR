using Refit;

namespace LiveQuotationSignalR.Gateways
{
    public interface IQuotationGateway
    {
        [Get("/last/{assetTicker}")]
        Task<Asset> GetAssetQuotationAsync(string assetTicker, CancellationToken cancellationToken);
    }
}
