var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;



// Add services to the container.

services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
