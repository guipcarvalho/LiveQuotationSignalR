using LiveQuotationSignalR.Infra.Bus;

namespace LiveQuotationSignalR.Domain.Events
{
    public record AssetQuotationChangedEvent : Event
    {
        public string? AssetTicker { get; init; }
        public decimal Quotation { get; init; }
    }
}
