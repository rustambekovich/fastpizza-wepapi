using FastPizza.DataAccess.Interfaces.Branches;
using FastPizza.DataAccess.Interfaces.Categories;
using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.DataAccess.Interfaces.Products;
using FastPizza.DataAccess.Interfaces.Useries;
using FastPizza.DataAccess.Repositories.Branches;
using FastPizza.DataAccess.Repositories.Categories;
using FastPizza.DataAccess.Repositories.Customers;
using FastPizza.DataAccess.Repositories.Products;
using FastPizza.DataAccess.Repositories.Useries;
using FastPizza.Service.Interfaces.Auth;
using FastPizza.Service.Interfaces.Branches;
using FastPizza.Service.Interfaces.Categories;
using FastPizza.Service.Interfaces.Common;
using FastPizza.Service.Interfaces.Notifications;
using FastPizza.Service.Interfaces.Products;
using FastPizza.Service.Interfaces.UserAuth;
using FastPizza.Service.Interfaces.Useries;
using FastPizza.Service.Services.Auth;
using FastPizza.Service.Services.Btanches;
using FastPizza.Service.Services.Categories;
using FastPizza.Service.Services.Common;
using FastPizza.Service.Services.Notification;
using FastPizza.Service.Services.Products;
using FastPizza.Service.Services.UserAuthService;
using FastPizza.Service.Services.Useries;
using FastPizza.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

//-> 
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IUser, UserRepository>();


builder.Services.AddScoped<IProductServise, ProductServise>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthServiceSMS, AuthServiceSMS>();
builder.Services.AddSingleton<IPhoneSender, SmsSender>();
builder.Services.AddScoped<IEmailsender, EmailSender>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenUserService, UserTokenService>();
builder.Services.AddScoped<IAuthUserServiceSMS, UserAuthServiceSMS>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IUserservice, UserService>();

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", config =>
    {
        config.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//->
builder.ConfigureJwtAuth();
builder.ConfigureSwaggerAuth();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseDeveloperExceptionPage();
app.MapControllers();

app.Run();
