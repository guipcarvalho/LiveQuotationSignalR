using MediatR;

namespace LiveQuotationSignalR.Infra.Bus
{

    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishEvent<T>(T @event, CancellationToken cancellationToken = default) where T : Event
        {
            return _mediator.Publish(@event, cancellationToken);
        }

        public Task<GenericResult> SendCommand<T>(T command, CancellationToken cancellationToken = default) where T : Command
        {
            return _mediator.Send(command, cancellationToken);
        }
    }

    public interface IMediatorHandler
    {
    }
}
