namespace SF.TelegramBot.Commands
{
    /// <summary>
    /// Команда с опциями
    /// </summary>
    public abstract class ChatTextCommandOption : AbstractCommand
    {
        public override bool CheckMessage(string message)
        {
            return message.StartsWith(CommandText);
        }

        public string ClearMessageFromCommand(string message)
        {
            if (message.Length > CommandText.Length + 1)
                return message.Substring(CommandText.Length + 1);
            else
                return "";
        }

    }
}
