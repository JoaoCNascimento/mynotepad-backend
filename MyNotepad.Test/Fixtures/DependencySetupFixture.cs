using MyNotepad.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public class DependencySetupFixture
{
    public DependencySetupFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfrastructure(new ConfigurationBuilder().Build());

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; private set; }
}