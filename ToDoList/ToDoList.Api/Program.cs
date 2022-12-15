using ToDoList.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbConnection();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCorsPolicy();
builder.Services.AddControllers();
builder.Services.RegisterSwagger();

var app = builder.Build();
app.RegisterSwaggerUsage();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();