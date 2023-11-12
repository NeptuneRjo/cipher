using Cipher.BLL;
using CipherApp.API;
using CipherApp.DAL;
using CipherApp.DAL.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var services = builder.Services;

services.RegisterAPIDependencies(builder.Configuration);
services.RegisterBLLDependencies(builder.Configuration);
services.RegisterDALDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    var scope = app.Services.CreateScope();
    var providerServices = scope.ServiceProvider;

    SeedData.Initialize(providerServices);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
