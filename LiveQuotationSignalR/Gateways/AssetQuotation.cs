using System.Text.Json.Serialization;

namespace LiveQuotationSignalR.Gateways;

public record Asset
{
    [JsonPropertyName("USDBRL")]
    public AssetQuotation? UsdBrl { get; set; }

    public record AssetQuotation
    {
        public string Name { get; set; }

        public string Bid { get; set; }

        public string Ask { get; set; }
    }
}
