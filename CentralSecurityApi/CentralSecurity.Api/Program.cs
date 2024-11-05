using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CentralSecurity.Api.Models.Mapping;
using CentralSecurity.Api.Services;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Commands;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Queries;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Infrastructure.Persistence;
using CentralSecurity.Infrastructure.Repositories;
using CentralSecurity.Infrastructure.Mapping;

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
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
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
builder.Services.AddScoped<IAuditService, AuditService>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthenticationCommands, AuthenticationCommands>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserCommands, UserCommands>();
builder.Services.AddScoped<IUserQueries, UserQueries>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleCommands, RoleCommands>();
builder.Services.AddScoped<IRoleQueries, RoleQueries>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuCommands, MenuCommands>();
builder.Services.AddScoped<IMenuQueries, MenuQueries>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();

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
