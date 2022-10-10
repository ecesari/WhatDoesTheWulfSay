using AutoMapper;
using WhatDoesTheWulfSay.Application.Common.Mappers;
using WhatDoesTheWulfSay.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using WhatDoesTheWulfSay.Infrastructure;
using WhatDoesTheWulfSay.Infrastructure.Repository;
using WhatDoesTheWulfSay.Application.Common.Interfaces;
using WhatDoesTheWulfSay.Application.Services;

var builder = WebApplication.CreateBuilder(args);

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

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<EcommerceManagementContext>();
    context.Database.EnsureCreated();  
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

app.Run();
