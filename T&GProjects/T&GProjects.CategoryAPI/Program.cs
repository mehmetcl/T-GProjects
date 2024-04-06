using T_GProjects.CategoryService.BussinessLayer.Abstract;
using T_GProjects.CategoryService.BussinessLayer.Concrete;
using T_GProjects.CategoryService.DataAccessLayer.Abstract;
using T_GProjects.CategoryService.DataAccessLayer.Concrete;
using T_GProjects.CategoryService.DataAccessLayer.EntityFramework;
using T_GProjects.CategoryService.DataAccessLayer.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services are added for dependency injection.
builder.Services.AddDbContext<CategoryDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericDal<>));
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();


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
