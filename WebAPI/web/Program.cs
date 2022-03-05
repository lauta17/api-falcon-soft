using application.usecases.orders;
using application.usecases.orders.abstractions;
using application.usecases.products;
using application.usecases.products.abstractions;
using domain.repositories.orders;
using domain.repositories.products;
using domain.repositories.users;
using infrastructure.database;
using infrastructure.database.abstractions;
using infrastructure.database.helpers;
using infrastructure.database.mappers;
using infrastructure.database.mappers.abstractions;
using infrastructure.database.repositories.orders;
using infrastructure.database.repositories.products;
using infrastructure.database.repositories.users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using web.authorization;
using web.authorization.abstractions;
using web.helpers;
using web.mappers;
using web.mappers.abstractions;
using web.mappers.authenticate;
using web.mappers.orders;
using web.mappers.products;
using web.swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
    opt.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer"
    });
    opt.OperationFilter<AuthenticationRequirementsOperationFilter>();
});

#region Database
builder.Services
    .AddScoped<ISqlDbContext, SqlDbContext>()
    .AddDbContext<SqlDbContext>(
        options => {
            options.UseInMemoryDatabase("DatabaseTest");
        });


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IGetProductByIdRepository, GetProductByIdRepository>();
builder.Services.AddScoped<IGetProductsRepository, GetProductsRepository>();
builder.Services.AddScoped<IGetOrdersRepository, GetOrdersRepository>();
builder.Services.AddScoped<IGetOrderByIdRepository, GetOrderByIdRepository>();
builder.Services.AddScoped<IGetUserByCredentialRepository, GetUserByCredentialRepository>();
builder.Services.AddScoped<IGetUserByIdRepository, GetUserByIdRepository>();


builder.Services.AddScoped<IUpdateProductRepository, UpdateProductRepository>();
builder.Services.AddScoped<IUpdateOrderRepository, UpdateOrderRepository>();

builder.Services.AddScoped<IProductMapper, ProductMapper>();
builder.Services.AddScoped<IOrderMapper, OrderMapper>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

#endregion

#region Application

builder.Services.AddScoped<IGetProducts, GetProducts>();
builder.Services.AddScoped<IGetProductById, GetProductById>();
builder.Services.AddScoped<IGetFilteredOrders, GetFilteredOrders>();
builder.Services.AddScoped<IModifyProduct, ModifyProduct>();

builder.Services.AddScoped<IAuthentication, Authentication>();

#endregion

#region Web

builder.Services.AddScoped<IProductResponseMapper, ProductResponseMapper>();
builder.Services.AddScoped<IProductRequestMapper, ProductRequestMapper>();
builder.Services.AddScoped<IOrderResponseMapper, OrderResponseMapper>();
builder.Services.AddScoped<IAuthenticateRequestMapper, AuthenticateRequestMapper>();
builder.Services.AddScoped<IAuthenticateResponseMapper, AuthenticateResponseMapper>();

builder.Services.AddScoped<IQueryStringPaginationMapper, QueryStringPaginationMapper>();
builder.Services.AddScoped<IQueryStringFiltersMapper, QueryStringFiltersMapper>();

#endregion


#region JWT Token

builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

#endregion

var app = builder.Build();

DataGenerator.Initialize(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//JWT
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.Run();
