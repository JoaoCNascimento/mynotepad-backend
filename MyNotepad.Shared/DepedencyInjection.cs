using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNotepad.Application.Services;
using MyNotepad.Domain.Config;
using MyNotepad.Domain.Interfaces.Repositories;
using MyNotepad.Domain.Interfaces.Services;
using MyNotepad.Domain.MapperProfile;
using MyNotepad.External.Handlers;
using MyNotepad.External.Handlers.Interfaces;
using MyNotepad.Identity;
using MyNotepad.Identity.Interfaces;
using MyNotepad.Persistence.Repositories;

namespace MyNotepad.Shared
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Inject the infrastruscture resources into the application services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {            
            services.Configure<RabbitMQConfig>(configuration.GetSection("RabbitMQ"));
            services.AddSingleton<IRabbitMQHandler, RabbitMQHandler>();

            services.AddLogging(logging => { logging.AddConsole(); logging.AddDebug(); });

            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddAutoMapper(typeof(MyNotepadProfile));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            services.AddDbContext<MyNotepadDbContext>();

            return services;
        }
    }
}
