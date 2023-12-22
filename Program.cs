using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TradingAutomaticoFx_Net.Domain.Entities;
using TradingAutomaticoFx_Net.Infrastructure.Context;
using TradingAutomaticoFx_Net.Infrastructure.Repositories;
using TradingAutomaticoFx_Net.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("tradingAutomaticoSql")));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbcontext>().AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var key = builder.Configuration.GetValue<string>("Kestrel:Certificates:Development:Password");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = "tu_issuer",
        //ValidAudience = "tu_audience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy => policy.AddPolicy("PolicyCors", build => 
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PolicyCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
