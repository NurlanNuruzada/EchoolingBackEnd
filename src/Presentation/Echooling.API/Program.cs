using Echooling.API.Middlewares;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Infrastructure;
using Echooling.Infrastructure.Services.Storage.Local;
using Echooling.Persistance;
using Echooling.Persistance.Contexts;
using Echooling.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.addStorage<LocalStorage>();
//builder.Services.addStorage<AzureStorage>();
builder.Services.addPersistanceServices();
builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IConfirmEmail, ConfirmEmail>();
builder.Services.AddScoped<INewsService, NewsServices>();
builder.Services.AddSingleton<IWebHostEnvironment>(env => builder.Environment);
builder.Services.AddLocalization();
List<CultureInfo> cultures = new() {
    new CultureInfo("es-ES"),
    new CultureInfo("eN-US"),
    new CultureInfo("ru-RU"),
};
RequestLocalizationOptions localizationOptions = new()
{
    ApplyCurrentCultureToResponseHeaders = true,
    SupportedCultures = cultures,
    SupportedUICultures = cultures
};
localizationOptions.SetDefaultCulture("en-US");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"])),
        LifetimeValidator = (_, expire, _, _) => expire > DateTime.UtcNow,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddInfrastuctureServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseRequestLocalization(localizationOptions);
using (var init = app.Services.CreateScope())
{
    var instance = init.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await instance.AppInitializer();
    await instance.RoleSeedAsync();
    await instance.CreateUserSeed();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.useCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
app.UseCors();
app.UseCors(cors => cors
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(x => true)
            .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();
string webRootPath = app.Environment.WebRootPath;
app.MapControllers();
app.Run();