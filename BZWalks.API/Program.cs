using AutoMapper;
using BZWalks.API.Data;
using BZWalks.API.Mappings;
using BZWalks.API.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<BZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BZWalksConnectionString")));

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

//builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddSingleton(new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfiles>();
}).CreateMapper());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
