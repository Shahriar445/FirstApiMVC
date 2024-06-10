using FirstApiMVC.DbContexts;
using FirstApiMVC.DependencyContainer;
using FirstApiMVC.IRepository;
using FirstApiMVC.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.



//Dependancy inversition 
// Register your DbContext
builder.Services.AddDbContext<ShopDbContext>();

var secretKey = "your_secret_key_here"; // Use a secret key stored securely


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



// JWT authentication configuration
var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});






// service for frontend 

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5501")
               .AllowAnyMethod()
               .AllowAnyHeader();
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
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles(); // for image file 
app.UseAuthentication(); // add authentication middleware 
app.UseAuthorization();

app.MapControllers();

app.Run();
