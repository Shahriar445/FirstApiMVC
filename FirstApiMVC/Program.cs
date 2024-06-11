using FirstApiMVC.DbContexts;
using FirstApiMVC.DependencyContainer;
using FirstApiMVC.IRepository;
using FirstApiMVC.jwttoken;
using FirstApiMVC.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.



// Register your DbContext
builder.Services.AddDbContext<ShopDbContext>();
// Register your repository through DependencyInversion 
DependencyInversion.RegisterServices(builder.Services);


var secretKey = "test_256_secret_key_32_character_need"; // Use a secret key stored securely

// JWT authentication configuration
var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services.AddSingleton(new TokenService(secretKey));

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
