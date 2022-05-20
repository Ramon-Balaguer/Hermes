using Hermes.Api;
using Hermes.Core;
var portsSmtp = new[] {25, 587};
var hermesServer = new HermesServer(portsSmtp);
hermesServer.Start();

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "MyAllowSpecificOrigins";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200");
            policy.AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/messages", () =>
{
    return hermesServer.ReceivedMessages().Select(message => new Hermes.Api.Message(
        message.Value.To.ToString(),
        message.Value.From.ToString(),
        message.Value.Subject,
        message.Value.Body.ToString(),
        message.ReceivedTime
        )).OrderByDescending(message => message.ReceivedTime);
})
.WithName("GetMessages");

app.MapDelete("/messages", () => hermesServer.DeleteAllMessages()).WithName("DeleteMessages");

app.MapGet("/configuration", () => new ServiceInformation(portsSmtp, "hermes.voxelgroup.net"))
    .WithName("GetConfiguration");

app.Run();