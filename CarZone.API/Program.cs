
using System.Text.Json.Serialization;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Mappers;
using CarZone.Application.Services;
using CarZone.Domain.Enums;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using CarZone.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });   
    c.UseInlineDefinitionsForEnums();
   
});
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IBrandRepository,BrandRepository>();
builder.Services.AddScoped<IModelRepository,ModelRepository>();
builder.Services.AddScoped<IListingRepository,ListingRepository>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IBrandService,BrandService>();
builder.Services.AddScoped<IModelService,ModelService>();
builder.Services.AddScoped<IListingService,ListingService>();




builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserAutoMapper>();
    cfg.AddProfile<BrandAutoMapper>();
    cfg.AddProfile<ModelAutoMapper>();
    cfg.AddProfile<ListingAutoMapper>();



});

builder.Services.AddDbContext<CarZoneDBContext>(options =>
{
    var ConnectionString=builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(ConnectionString);
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>{
    
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();



app.Run();