using FirstApiMVC.DbContexts;
using FirstApiMVC.IRepository;
using FirstApiMVC.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register your DbContext
builder.Services.AddDbContext<ShopDbContext>();

// Register your repository
builder.Services.AddScoped<IShopRepository, ShopRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
