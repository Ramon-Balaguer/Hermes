using Hermes.Core;
var hermesServer = new HermesServer(25, 587);
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

app.Run();
