using Hermes.Api;
using Hermes.Core;
var portsSmtp = new[] {25, 587};
var hermesServer = new HermesServer(portsSmtp);
hermesServer.Start();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/messages", () =>
{
    return hermesServer.ReceivedMessages().Select(message => new Hermes.Api.Message(
        message.Value.Body.ToString(),
        message.Value.To.ToString(),
        message.Value.From.ToString(),
        message.Value.Subject.ToString(),
        message.ReceivedTime
        )).OrderByDescending(message => message.ReceivedTime);
})
.WithName("GetMessages");

app.MapGet("/configuration", () => new Configuration(portsSmtp, "hermes.voxelgroup.net"))
    .WithName("GetConfiguration");

app.Run();