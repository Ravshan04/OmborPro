using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using OmborPro.Application;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Infrastructure.Data;
using OmborPro.Infrastructure.Services;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB serialization once for Guid fields.
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var mongoSection = builder.Configuration.GetSection("MongoDbSettings");
var mongoSettings = mongoSection.Get<MongoDbSettings>() ?? new MongoDbSettings();
if (string.IsNullOrWhiteSpace(mongoSettings.ConnectionString) || string.IsNullOrWhiteSpace(mongoSettings.DatabaseName))
{
    throw new InvalidOperationException("MongoDbSettings.ConnectionString and MongoDbSettings.DatabaseName must be configured.");
}

builder.Services.Configure<MongoDbSettings>(mongoSection);
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplication();

builder.Services.AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? "a-very-long-secret-key-for-stock-harmony-42");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"]
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
