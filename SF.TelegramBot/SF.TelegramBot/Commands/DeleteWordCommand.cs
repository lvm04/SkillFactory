namespace SF.TelegramBot.Commands
{
    public class DeleteWordCommand : ChatTextCommandOption, IChatTextCommandWithAction
    {
        public DeleteWordCommand()
        {
            CommandText = "/deleteword";
        }

        public bool DoAction(Conversation chat, out string resultMessage)
        {
            var message = chat.GetLastMessage();
            var text = ClearMessageFromCommand(message);

            if (string.IsNullOrEmpty(text))
            {
                resultMessage = "Отсутствует параметр команды. ";
                return false;
            }
                
            if (Conversation.dictionary.ContainsKey(text))
            {
                Conversation.dictionary.Remove(text);
                resultMessage = "";
                return true;
            }
            else
            {
                resultMessage = $"Слово <i>{text}</i> не найдено в словаре. ";
            }

            return false;
        }

        public string ReturnText()
        {
            return "Слово успешно удалено!";
        }
    }
}
