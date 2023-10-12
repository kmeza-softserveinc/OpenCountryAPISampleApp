using Microsoft.EntityFrameworkCore;
using OpenCountryAPISampleApp.EFModels.UsersModel;
using OpenCountryAPISampleApp.Interfaces.IRepositories;
using OpenCountryAPISampleApp.Interfaces.IServices;
using OpenCountryAPISampleApp.Repositories;
using OpenCountryAPISampleApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc();

// Add services to the container.
builder.Services.AddHttpClient<IRestCountriesRepository, RestCountriesRepository>();
builder.Services.AddTransient<IRestCountriesService, RestCountriesService>();

// Add DB Context
builder.Services.AddDbContext<UsersDbContext>(options =>
        options.UseSqlite("Data Source=Data/UsersDatabase/Users.sqlite")
);

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
        {
            options.Cookie.Name = "UserLoginCookie";
            options.LoginPath = "/Login";
        }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyMethod(); // or builder.WithMethods("POST", ...);
        // ... other settings ...
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=}/{action=Index}/{id?}");

app.Run();
