using Microsoft.Extensions.Logging;

namespace LiveQuotationSignalR.Infra.Bus;

public interface IMediatorHandler
{
    Task<GenericResult> SendCommand<T>(T command, CancellationToken cancellationToken = default) where T : Command;
    Task PublishEvent<T>(T @event, CancellationToken cancellationToken = default) where T : Event;
}