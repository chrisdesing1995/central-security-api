using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CentralSecurity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var corsOrigin = "corsOrigin";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

//builder.Services.AddAutoMapper(typeof(Program));

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
//builder.Services.AddScoped<IAuthRepository, AuthRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IMenuRepository, MenuRepository>();
//builder.Services.AddScoped<IRolRepository, RolRepository>();
//builder.Services.AddScoped<IExecuteStoreProcedure, ExecuteStoreProcedure>();
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
