using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;

namespace LiveQuotationSignalR.Hubs
{
    public class QuotationHub : Hub
    {
        private readonly ConcurrentDictionary<string, IList<ISingleClientProxy>> _assetSubscribers;

        public QuotationHub()
        {
            _assetSubscribers = new ConcurrentDictionary<string, IList<ISingleClientProxy>>();
        }

        public void SubscribeAssetQuotation(string assetTicker)
        {
            
            _assetSubscribers.AddOrUpdate(assetTicker, new List<ISingleClientProxy>() { Clients.Caller }, (ticker, subscribers) =>
            {
                subscribers.Add(Clients.Caller);

                return subscribers;
            });
        }

        public void SendQuotationToSubscribers(string assetTicker, decimal value)
        {

            var subscribers = _assetSubscribers.GetValueOrDefault(assetTicker);

            if (subscribers == null || !subscribers.Any()) return;

            foreach (var subscriber in subscribers)
            {
                _ = subscriber.SendAsync("AssetQuotation", assetTicker, value);
            }
        }
    }
}
