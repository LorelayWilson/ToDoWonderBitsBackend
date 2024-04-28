using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ToDoWonderBitsBackend.Application.Handlers;
using ToDoWonderBitsBackend.Application.Handlers.Interfaces;
using ToDoWonderBitsBackend.Application.Services;
using ToDoWonderBitsBackend.Application.Services.Interfaces;
using ToDoWonderBitsBackend.Domain.Ports;
using ToDoWonderBitsBackend.Infrastructure.Persistence;
using ToDoWonderBitsBackend.Infrastructure.Persistence.Repositories;
using ToDoWonderBitsBackend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TodoContext>();
//Repositorios y Servicios (IoC)
builder.Services.AddScoped<ITodoItemCommandHandler, TodoItemCommandHandler>();
builder.Services.AddScoped<ITodoItemQueryHandler, TodoItemQueryHandler>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IUserCommandHandler, UserCommandHandler>();
builder.Services.AddScoped<IUserQueryHandler, UserQueryHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//mapper
builder.Services.AddAutoMapper(typeof(Program));
// JWT
// Agrega el servicio de autenticación con JWT.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// En el método ConfigureServices del archivo Program.cs

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Agregar un filtro para incluir el token JWT en Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
