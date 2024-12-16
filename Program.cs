using Microsoft.EntityFrameworkCore;
using EvalApi.Src.Core.Repositories;
using EvalApi.Src.Core.Services;

namespace MovieAppApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ajouter les services au conteneur
            builder.Services.AddControllers();

            // Enregistrement de la base de données SQLite
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=Data/database.db");
            });

            // Enregistrement des services utilisateur
            builder.Services.AddScoped<IUserService, UserService>();
             // Enregistrement du repository utilisateur
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPostService, PostService>();

            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            // Enregistrement de Swagger pour l'API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Ajouter les services CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost3000", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // Autoriser les requêtes depuis ce domaine
                        .AllowAnyMethod()                    // Autoriser toutes les méthodes HTTP (GET, POST, PUT, DELETE, etc.)
                        .AllowAnyHeader();                   // Autoriser tous les en-têtes HTTP
                });
            });
            var app = builder.Build();
            // Utiliser la politique CORS avant d'utiliser les autres middlewares
            app.UseCors("AllowLocalhost3000");
            // Ajouter le middleware de gestion d'exception
            app.UseMiddleware<ExceptionHandlingMiddleware>();   
            // Configure le pipeline des requêtes HTTP
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
