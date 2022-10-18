using Microsoft.EntityFrameworkCore;
using HekaMiniumApi.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Certificate;
using HekaMiniumApi.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services.AddDbContext<HekaMiniumSchema>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("HekaMiniumData")));

// AUTO-MIGRATE ON STARTUP
SchemaFactory.ConnectionString = builder.Configuration.GetConnectionString("HekaMiniumData");
SchemaFactory.ApplyMigrations();

// Apply JWT authentication configuration
string apiKey = "HekaMinium2021HekaButan2022 IS THE SCRET KEY OF THE COMPANY";
// builder.Services.AddAuthentication(
//         CertificateAuthenticationDefaults.AuthenticationScheme)
//         .AddCertificate();
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer=false,
            ValidateAudience=false,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(apiKey)),
        };
    });
builder.Services.AddSingleton<HekaAuth>(new HekaAuth(apiKey));
builder.Services.AddAuthorization(options =>
{
   options.AddPolicy("SysAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "SysAdmin"));
   options.AddPolicy("WebUser", policy => policy.RequireClaim(ClaimTypes.Role, "WebUser"));
   options.AddPolicy("Operator", policy => policy.RequireClaim(ClaimTypes.Role, "Operator"));
   options.AddPolicy("Device", policy => policy.RequireClaim(ClaimTypes.Role, "Device"));
});

// Add CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition");
        });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseSetting("https_port", "443");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
// app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
