using AutoMapper;
using EverybodyCodes.Application.Common.Interfaces;
using EverybodyCodes.Application.Common.Mappers;
using EverybodyCodes.Application.Models.Camera;
using EverybodyCodes.Application.Services;
using EverybodyCodes.Domain.Repositories;
using EverybodyCodes.Infrastructure;
using EverybodyCodes.Infrastructure.Csv;
using EverybodyCodes.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:8080");
                      });
});

builder.Services.AddControllers();

//db
builder.Services.AddDbContext<CameraDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("CameraDb")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//automapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperConfig());

});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//Dependencies
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<ICameraRepository, CameraRepository>();
builder.Services.AddTransient<ICameraService, CameraService>();



var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CameraDbContext>();
    context.Database.EnsureCreated();
    if (!context.Cameras.Any())
    {
        var csvReader = new CsvReader<CameraInsertCommand>();
        var reader = new FileStream("External\\cameras-defb.csv", FileMode.Open);
        var cameras = csvReader.Read(reader);
        var camService = serviceScope.ServiceProvider.GetService<ICameraService>();
        await camService.BulkAdd(cameras.ToList());
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
