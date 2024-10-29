using YourNamespace.Services;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<MqttService>();
// Add services to the container.S

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
