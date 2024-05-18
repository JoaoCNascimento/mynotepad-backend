using Microsoft.Extensions.DependencyInjection;
using MyNotepad.External.Handlers.Interfaces;

namespace MyNotepad.Test.External.Handler;
public class RabbitMQHandler_Test : IClassFixture<DependencySetupFixture>
{
    private readonly ServiceProvider _serviceProvide;

    public RabbitMQHandler_Test(DependencySetupFixture fixture)
    {
        _serviceProvide = fixture.ServiceProvider;
    }

    [Theory(DisplayName = "Should Send Message Successfully")]
    [InlineData("Teste", "user_email_validation")]
    [InlineData("{ \"emailAddress\": \"teste@email.com\" }", "user_email_validation")]
    public void ShouldSendMessageSuccessfully(string message, string queue)
    {
        using (var scope = _serviceProvide.CreateScope())
        {
            var producer = scope.ServiceProvider.GetService<IRabbitMQHandler>()!;

            producer.SendMessage(message, queue);
        }
    }
}

