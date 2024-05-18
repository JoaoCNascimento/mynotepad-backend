namespace MyNotepad.External.Handlers.Interfaces
{
    public interface IRabbitMQHandler
    {
        public void SendMessage(string message, string queueName);
        public string ReceiveMessage(Action<string> messageHandler, string queueName);
    }
}
