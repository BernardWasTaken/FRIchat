using FRIchat.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FRIchat.Data;
using FRIchat.Hubs;
using FRIchat.Models;
using FRIchat.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using ApiController = System.Web.Http.ApiController;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FRIchatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureContext") ?? throw new InvalidOperationException("Connection string 'AzureContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddScoped<IOdgovorService, OdgovorService>();
builder.Services.AddScoped<OdgovorController>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDefaultIdentity<Uporabnik>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FRIchatContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");

app.Run();
