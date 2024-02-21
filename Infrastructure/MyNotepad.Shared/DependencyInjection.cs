using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyNotepad.Domain.Interfaces;
using MyNotepad.Persistence.Repositories;
using MyNotepad.Application.Interfaces;
using MyNotepad.Application.Services;
using MyNotepad.Application.Mappings;

namespace MyNotepad.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }

    public static IServiceCollection AddEntityToDTOMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(EntityToDTOMappingProfile));
        return services;
    }
}
