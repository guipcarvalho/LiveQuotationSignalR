using LiveQuotationSignalR.Domain;
using LiveQuotationSignalR.Domain.Events;
using LiveQuotationSignalR.Gateways;
using LiveQuotationSignalR.Infra;
using LiveQuotationSignalR.Infra.Bus;
using LiveQuotationSignalR.Infra.Hubs;
using LiveQuotationSignalR.Worker;
using MediatR;
using Microsoft.OpenApi.Models;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Live Quotation API", Version = "v1" });

    options.AddSignalRSwaggerGen();
});

builder.Services.AddSignalR();
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddSingleton<INotificationManager, SignalRNotificationManager>();
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddScoped<INotificationHandler<AssetQuotationChangedEvent>, AssetQuotationChangedEventHandler>();

builder.Services.AddRefitClient<IQuotationGateway>()
    .ConfigureHttpClient(
        c => c.BaseAddress = new Uri(builder.Configuration["UrlQuotationService"]));

builder.Services.AddHostedService<QuotationBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapHub<QuotationHub>("/quotationHub");

app.Run();
