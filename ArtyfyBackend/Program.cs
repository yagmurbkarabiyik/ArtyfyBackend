using ArtyfyBackend.API.Modules;
using ArtyfyBackend.Bll.Mapping;
using ArtyfyBackend.Dal.Context;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using ArtyfyBackend.Domain.Token;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ArtyfyBackend.Bll.Services;
using ArtyfyBackend.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB connection
builder.Services.AddDbContext<ArtyfyBackendDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("ArtyfyBackendConnectionString"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(ArtyfyBackendDbContext)).GetName().Name);
    });
});

//Token Options integration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenOptions"));

//Authentication ve token implementasyonu
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenSettings>();
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

//Identity implementasyonu
builder.Services.AddIdentity();

//Automapper implemantasyonu
builder.Services.AddAutoMapper(typeof(MapProfile));

//Autofac implemantasyonu
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new RepositoryServiceModule()));

//swagger api key implementasyonu
builder.Services.AddSwaggerAuthorization();

var app = builder.Build(); ;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowedOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();

app.MapControllers();

app.Run();