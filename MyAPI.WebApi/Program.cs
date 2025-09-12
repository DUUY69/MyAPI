using Microsoft.EntityFrameworkCore;
using MyAPI.Repositories;
using MyAPI.Repositories.Entities;
using MyAPI.Repositories.Interfaces;
using MyAPI.Services;
using MyAPI.Services.Common;
using MyAPI.Services.Interfaces;
using MyAPI.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Start DI
builder.Services.AddSingleton<IHashHelper, HashHelper>();

// AppSettings
var appSettings = new AppSettings
{
    ApplicationUrl = builder.Configuration["AppSettings:ApplicationUrl"],
    LandingPageUrl = builder.Configuration["AppSettings:LandingPageUrl"],
    EmailSettings = new EmailSettings
    {
        SmtpServer = builder.Configuration["AppSettings:EmailSettings:SmtpServer"],
        SmtpPort = int.Parse(builder.Configuration["AppSettings:EmailSettings:SmtpPort"]),
        SenderEmail = builder.Configuration["AppSettings:EmailSettings:SenderEmail"],
        Password = builder.Configuration["AppSettings:EmailSettings:Password"],
        SenderName = builder.Configuration["AppSettings:EmailSettings:SenderName"],
        Username = builder.Configuration["AppSettings:EmailSettings:Username"]
    }
};

builder.Services.AddSingleton(appSettings);

var cnnString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyAPIContext>((optionBuilder) =>
{
    optionBuilder.UseSqlServer(cnnString);
});

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register Services
builder.Services.AddScoped<IUserService, UserService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Use CORS
app.UseCors("AllowFrontend");

app.MapControllers();

await app.RunAsync();
