using prog7311.Repository;
using prog7311.Services;

/*
 * This is the Program.cs file. It is the entry point of the program. It is where all of the dependencies
 * are injected. A bunch of MVC Asp Net framework options are set and the app runs.
 */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register our services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IFarmerService, FarmerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Automatically redirect http requests to https.
app.UseHttpsRedirection(); 

// Enable the built in routing to match incoming requests to the correct endpoints.
app.UseRouting();

// Enable the built in authorisation middleware.
app.UseAuthorization();

// Map the static assets such as images to be served by the web server
app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Run the app.
app.Run();
