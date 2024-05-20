using Havit.Blazor.Components.Web;
using Microsoft.EntityFrameworkCore;
using UnitTestingDemo2024.Components;
using UnitTestingDemo2024.Models;
using UnitTestingDemo2024.Repositories.Sales;
using UnitTestingDemo2024.Services.Sales;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("demo"));

// Add application services.
builder.Services.AddTransient<IPriceResolver, PriceResolver>();

// repositories
builder.Services.AddTransient<IBasicPriceRepository, BasicPriceRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerPriceRepository, CustomerPriceRepository>();
builder.Services.AddTransient<ICustomerProductGroupDiscountRepository, CustomerProductGroupDiscountRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddHxServices();
builder.Services.AddHxMessenger();
builder.Services.AddHxMessageBoxHost();

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

// Initial data seed
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
	var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
	context.EnsureSeedData();
}

app.Run();
