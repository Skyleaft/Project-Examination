using API.Database;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder
    .Services
    .AddAuthenticationJwtBearer(o => o.SigningKey = builder.Configuration["Auth:SigningKey"])
    .AddAuthorization()
    .AddFastEndpoints();
builder.Services.AddTransient<GenericRepository>();
builder.Services.Configure<JsonOptions>(o =>
    o.SerializerOptions.PropertyNamingPolicy = null);
builder.Services.SwaggerDocument(
        d => d.DocumentSettings =
                 s =>
                 {
                     s.DocumentName = "v0";
                     s.Version = "0.0.0";
                 });



var app = builder.Build();

app.MapDefaultEndpoints();



app.Run();
