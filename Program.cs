using Microsoft.EntityFrameworkCore;
using api.Core.Interfaces;
using api.Core.Services;
using api.Data;

using api.Data.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500", "https://emsijoseph.github.io")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Category Service
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Product Service
builder.Services.AddScoped<IProductService, ProductService>();

// Product Images Service
builder.Services.AddScoped<IProductImageService, ProductImageService>();

// Account Service
builder.Services.AddScoped<IAccountService, AccountService>();

// User Service
builder.Services.AddScoped<IUserService, UserService>();

// Cart Service
builder.Services.AddScoped<ICartService, CartService>();

// Order Service
builder.Services.AddScoped<IOrderService, OrderService>();

// Card Service
builder.Services.AddScoped<ICardService, CardService>();

var app = builder.Build();

// Add this section for seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    await DatabaseSeeder.SeedDatabase(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Add CORS middleware here - before routing middleware
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

