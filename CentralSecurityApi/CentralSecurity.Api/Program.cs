using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CentralSecurity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Api.Services;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Commands;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Queries;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Infrastructure.Repositories;
using CentralSecurity.Infrastructure.Mapping;
using CentralSecurity.Api.Models.Mapping;

var builder = WebApplication.CreateBuilder(args);
var corsOrigin = "corsOrigin";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Add DbContext.
builder.Services.AddDbContext<CentralSecurityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

// Add Authentication services.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: builder.Configuration["Jwt:key"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
    };
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfileApi>();
    cfg.AddProfile<MappingProfile>();
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOrigin,
        b =>
        {
            var origins = builder.Configuration["App:CorsOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (origins != null && origins.Length > 0)
            {
                b.WithOrigins(origins)
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            }
        });
});

// Add Repositories.
#region Module Security
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleCommands, RoleCommands>();
builder.Services.AddScoped<IRoleQueries, RoleQueries>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserCommands, UserCommands>();
builder.Services.AddScoped<IUserQueries, UserQueries>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsOrigin);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
