using Microsoft.Extensions.Logging;

namespace LiveQuotationSignalR.Infra.Bus;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T @event, CancellationToken cancellationToken = default) where T : Event;
}