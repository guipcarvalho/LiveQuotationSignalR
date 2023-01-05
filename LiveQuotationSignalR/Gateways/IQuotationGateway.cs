using Refit;

namespace LiveQuotationSignalR.Gateways
{
    public interface IQuotationGateway
    {
        [Get("/last/{assetTicker}")]
        Task<ApiResponse<Asset>> GetAssetQuotationAsync(string assetTicker, CancellationToken cancellationToken);
    }
}
