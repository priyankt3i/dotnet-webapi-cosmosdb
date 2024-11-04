
using DotNet_WebAPI_CosmosDB.Data;
using DotNet_WebAPI_CosmosDB.Repository;
using Microsoft.EntityFrameworkCore;

namespace DotNet_WebAPI_CosmosDB
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped<IGamesRepo,GamesRepository>();
			builder.Services.AddDbContext<GameContext>(Options =>
			{
				var cosmosurl = builder.Configuration.GetValue<string>("CosmosDB:URL");
				var accesskey = builder.Configuration.GetValue<string>("CosmosDB:AccessKey");
				Options.UseCosmos(cosmosurl, accesskey, databaseName: "Sample-Db");
			});
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
		}
	}
}
