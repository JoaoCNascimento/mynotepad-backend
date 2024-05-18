namespace MyNotepad.Domain.Enums
{
    public enum QueuesNames { UserEmailValidation, EmailConfirmationWasSentToUser };

    public static class QueuesNamesExtensions
    {
        public static string GetQueueName(this QueuesNames queueName)
        {
            switch (queueName)
            {
                case QueuesNames.UserEmailValidation: return "user_email_validation";
                case QueuesNames.EmailConfirmationWasSentToUser: return "email_confirmation_sent_to_user";
                default: throw new ArgumentOutOfRangeException("queue");
            }
        }
    }
}
