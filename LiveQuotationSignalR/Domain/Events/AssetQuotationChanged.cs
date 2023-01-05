using LiveQuotationSignalR.Infra.Bus;

namespace LiveQuotationSignalR.Domain.Events
{
    public record AssetQuotationChanged : Event
    {
        public string AssetTicker { get; init; }
        public decimal Quotation { get; init; }
    }
}
