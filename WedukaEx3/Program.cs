using Microsoft.Extensions.DependencyInjection;
using WedukaEx3.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<PessoaService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7249/");
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
