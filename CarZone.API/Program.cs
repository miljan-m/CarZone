
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Mappers;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using CarZone.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGenericRepository<User>,GenericRepository<User>>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserAutoMapper>();
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