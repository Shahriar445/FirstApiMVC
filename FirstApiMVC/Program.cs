using FirstApiMVC.DbContexts;
using FirstApiMVC.DependencyContainer;
using FirstApiMVC.IRepository;
using FirstApiMVC.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.




// Register your DbContext
builder.Services.AddDbContext<ShopDbContext>();



/*
//Enviorment Set For Docker Container  

var DbHost = Environment.GetEnvironmentVariable("DB_HOST");
var DbName = Environment.GetEnvironmentVariable("DB_NAME");
var DbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

if (builder.Environment.IsDevelopment())
{

    builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

}
else
{

    builder.Services.AddDbContext<ShopDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
*/

// Register your repository through DependencyInversion 
DependencyInversion.RegisterServices(builder.Services);
























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
