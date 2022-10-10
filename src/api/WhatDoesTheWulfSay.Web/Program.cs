using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WhatDoesTheWulfSay.Application.Common.Interfaces;
using WhatDoesTheWulfSay.Application.Common.Mappers;
using WhatDoesTheWulfSay.Application.Services;
using WhatDoesTheWulfSay.Domain.Repositories;
using WhatDoesTheWulfSay.Infrastructure;
using WhatDoesTheWulfSay.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//db
builder.Services.AddDbContext<EcommerceManagementContext>(x => x.UseInMemoryDatabase("ECommerceDb"));

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
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IReviewService, ReviewService>();

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
