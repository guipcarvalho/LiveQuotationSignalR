using MediatR;

namespace LiveQuotationSignalR.Infra.Bus
{
    public abstract record Event : INotification
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; }
    }
}
