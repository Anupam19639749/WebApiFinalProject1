using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;
using WebApiFinalProject1.Repository;
using WebApiFinalProject1.Service;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering repositories and services for dependency injection
builder.Services.AddScoped<IBloggingAPI<User>, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IBloggingAPI<Profile>, ProfileRepository>();
builder.Services.AddScoped<ProfileService>();

builder.Services.AddScoped<IBloggingAPI<Post>, PostRepository>();
builder.Services.AddScoped<PostService>();

builder.Services.AddScoped<IBloggingAPI<Comment>, CommentRepository>();
builder.Services.AddScoped<CommentService>();

// Adding the DbContext with SQL Server provider
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));

// To avoid cyclical reference error
builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler =
                            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
