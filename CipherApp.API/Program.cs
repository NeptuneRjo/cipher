using Cipher.BLL;
using CipherApp.DAL;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllers();

services.RegisterDALDependencies(builder.Configuration);
services.RegisterBLLDependencies(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
