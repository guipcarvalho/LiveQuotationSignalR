using LiveQuotationSignalR.Domain;
using LiveQuotationSignalR.Infra;
using LiveQuotationSignalR.Infra.Bus;
using LiveQuotationSignalR.Infra.Hubs;
using MediatR;
using Microsoft.OpenApi.Models;

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
