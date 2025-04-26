
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDependencyInjection(builder);
builder.Services.AddMediatRService();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
    app.MapScalarApiReference(option =>
    {
        option.Title = "Digital Brooker API";
    });
}
app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
