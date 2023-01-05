using LiveQuotationSignalR.Domain.Events;
using LiveQuotationSignalR.Gateways;
using LiveQuotationSignalR.Infra.Bus;

namespace LiveQuotationSignalR.Worker
{
    public class QuotationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<QuotationBackgroundService> _logger;

        public QuotationBackgroundService(IServiceProvider serviceProvider, ILogger<QuotationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Quotation Service started");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Quotation Service executing");
            using var scope = _serviceProvider.CreateScope();

            var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var quotationGateway = scope.ServiceProvider.GetRequiredService<IQuotationGateway>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    const string assetTicker = "USD-BRL";
                    var quotation = await quotationGateway.GetAssetQuotationAsync(assetTicker, stoppingToken);

                    if (quotation?.UsdBrl?.Ask is null)
                        throw new ArgumentException(nameof(quotation));

                    await mediatorHandler.PublishEvent(new AssetQuotationChangedEvent
                        { AssetTicker = assetTicker, Quotation = quotation.UsdBrl.Ask }, stoppingToken);

                    _logger.LogInformation("Quotation sent");
                    
                }
                catch (Exception e)
                {
                    _logger.LogError(
                        "Error trying to fetch asset quotation", e);
                    throw;
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Quotation Service stopped");

            return base.StopAsync(cancellationToken);
        }
    }
}
