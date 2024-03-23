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
using ArtyfyBackend.API.Middlewares;
using Peticom.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenOptions"));

//Authentication and token implementation
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

builder.Services.AddIdentity();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new RepositoryServiceModule()));

builder.Services.AddSwaggerAuthorization();

var app = builder.Build(); ;

 app.UseSwagger();

 app.UseSwaggerUI();

app.UseCors("MyAllowedOrigins");

app.UseMiddleware<ApiKeyAuthorizationMiddleware>();

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();

app.MapControllers();

app.Run();