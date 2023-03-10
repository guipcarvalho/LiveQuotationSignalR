using LiveQuotationSignalR.Infra.Bus;

namespace LiveQuotationSignalR.Domain.Events
{
    public record AssetQuotationChangedEvent : Event
    {
        public string? AssetTicker { get; init; }
        public string? Quotation { get; init; }
    }
}
