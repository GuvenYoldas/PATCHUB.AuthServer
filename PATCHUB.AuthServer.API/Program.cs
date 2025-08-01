using PATCHUB.AuthServer.Persistence;
using PATCHUB.AuthServer.Infrastructure;
using PATCHUB.AuthServer.Persistence.Configurations;
using PATCHUB.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PATCHUB.SharedLibrary.Services;
using PATCHUB.SharedLibrary.ErrorHandling.Middleware;
using PATCHUB.SharedLibrary.ErrorHandling;
using PATCHUB.SharedLibrary.Abstractions;
using PATCHUB.AuthServer.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IClientCredentialAccessor, ClientCredentialAccessor>();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddExceptionHandling();

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudiences = tokenOptions.Audience,
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        //ClockSkew = TimeSpan.Zero
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllFrontend",
                build => build
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patchub SafeSign v1");
       // c.RoutePrefix = string.Empty; // root'a alýr
    });
}


app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAllFrontend");

app.UseAuthentication(); // eksik dendi ama olmayýnca da çalýþýyo?
app.UseAuthorization();

app.MapControllers();
app.Run();

