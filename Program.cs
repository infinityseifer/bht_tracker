using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using backend.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- Database (keep if you still use MySQL anywhere) ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// --- ADDED: HttpClient for the Google Sheets proxy controller ---
builder.Services.AddHttpClient();

// --- CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
    );
});

// --- MVC/Controllers & Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student Behavior Tracker API",
        Version = "v1"
    });
});

var app = builder.Build();

// --- Reverse proxy headers (Cloud Run / nginx / etc.) ---
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// --- Swagger in Dev only ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Behavior Tracker API v1");
        c.RoutePrefix = "swagger";
    });
}

// --- Static files (serve wwwroot/index.html, etc.) ---
app.UseDefaultFiles();   // looks for index.html
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");

// app.UseHttpsRedirection(); // optional
app.UseAuthorization();

app.MapControllers();

// Optional SPA fallback if you want any unknown path to load index.html
// app.MapFallbackToFile("index.html");

app.Run();
