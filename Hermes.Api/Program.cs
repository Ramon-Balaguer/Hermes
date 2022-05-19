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
    return hermesServer.ReceivedMessages().Select(message => new Hermes.Api.Message()
    {
        Body = message.Value.Body.ToString(),
        To = message.Value.To.ToString(),
        From = message.Value.From.ToString(),
        Subject = message.Value.Subject.ToString(),
        ReceivedTime = message.ReceivedTime
    }).OrderByDescending(message => message.ReceivedTime);
})
.WithName("GetMessages");

app.MapGet("/configuration", () => new
    {
        Ports = portsSmtp,
        Name = "hermes.voxelgroup.net"
    })
    .WithName("GetConfiguration");

app.Run();
