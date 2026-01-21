
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.OpenApi.Models; // Ensure this is included

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
	opt.UseInMemoryDatabase("TodoList"));

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
});

var app = builder.Build();

// Configure the middleware
if (app.Environment.IsDevelopment())
{
	app.UseSwagger(); // Enable Swagger middleware

	// Specify the correct endpoint for Swagger UI
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1"); // Use this for endpoint
	});
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
