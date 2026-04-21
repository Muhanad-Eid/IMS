using Application.Repositories;
using Application.Services.CategoryService;
using Application.Services.Interfaces;
using Application.Services.ProductService;
using Application.Services.RoleService;
using Application.Services.TransactionInventoryService;
using Application.Services.UserService;
using Infrastructure.Context;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>c.SwaggerDoc("v1",new OpenApiInfo { Title="IMS API",Version="v1"}));
// Register the open generic types for the generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IRoleService,RoleService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ITransactionInventoryService, TransactionInventoryService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
UserSeedData.UserSeed(app.Services);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
