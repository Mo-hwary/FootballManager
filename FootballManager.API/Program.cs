using FootballManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FootballManager.Core.Interfaces;
using FootballManager.Infrastructure.Repositories;
using FootballManager.Services.Services;
using FootballManager.Services.Services;
using FootballManager.Core.InterfacesRepo;
using FootballManager.Core.InterfacesServices;


var builder = WebApplication.CreateBuilder(args);

//Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Services
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IStatsRepo, StatsRepo>();


//connection string 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
