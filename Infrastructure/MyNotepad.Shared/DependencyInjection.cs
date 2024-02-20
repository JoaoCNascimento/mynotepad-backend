using MyNotepad.Persistence.Interfaces;
using MyNotepad.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MyNotepad.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INoteRepository, NoteRepository>();
        return services;
    }
}
