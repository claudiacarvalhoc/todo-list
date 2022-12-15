using ToDoList.Core.Dtos;
using ToDoList.Core.Settings;
using TodoList.Repository;
using TodoList.Services;

namespace ToDoList.Api.Helpers;

public static class Extension
{
    /// <summary>
    /// Generate swagger based on registered endpoints. 
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    public static void RegisterSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    
    /// <summary>
    /// Generate usage of swagger UI when local environment is development.  
    /// </summary>
    /// <param name="app">An instance of <see cref="WebApplication"/>.</param>
    public static void RegisterSwaggerUsage(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
    
    /// <summary>
    /// Register database settings.
    /// </summary>
    /// <param name="builder">An instance of <see cref="WebApplicationBuilder"/>.</param>
    public static void AddDbConnection(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<TodoStoreDatabaseSettings>(
            builder.Configuration.GetSection("TodoStoreDatabase"));
    }
    
    /// <summary>
    /// Register services used by controllers.
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IService<TodoDto>, TodoService>();
    }
    
    /// <summary>
    /// Register repositories used by services.
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITodoRepository, MongoDbClientTodoCollectionRepository>();
    }
    
    /// <summary>
    /// Configure Cors policy to allow any host to request data to the api.
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(option =>
            option.AddPolicy("AppPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
    }
}