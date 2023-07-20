using FastPizza.DataAccess.Interfaces;
using FastPizza.DataAccess.Interfaces.Categories;
using FastPizza.DataAccess.Interfaces.Products;
using FastPizza.DataAccess.Repositories.Categories;
using FastPizza.DataAccess.Repositories.Products;
using FastPizza.Service.Interfaces.Categories;
using FastPizza.Service.Interfaces.Common;
using FastPizza.Service.Interfaces.Products;
using FastPizza.Service.Services.Categories;
using FastPizza.Service.Services.Common;
using FastPizza.Service.Services.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-> 
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductServise, ProductServise>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();




//->

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
