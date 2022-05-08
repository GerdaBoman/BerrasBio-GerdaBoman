using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BerrasBio.Data;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BerrasBioContext"); 

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Home", "");
});


builder.Services.AddDbContext<BerrasBioContext>(options =>
    options.UseSqlServer(connectionString));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BerrasBioContext>();
    context.Database.EnsureCreated();
    DataSeeding.SeedData(context);

}


app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
