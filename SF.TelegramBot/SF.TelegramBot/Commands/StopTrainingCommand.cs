namespace SF.TelegramBot.Commands
{
    public class StopTrainingCommand : AbstractCommand, IChatTextCommandWithAction
    {
        public StopTrainingCommand()
        {
            CommandText = "/stop";
        }

        public bool DoAction(Conversation chat, out string resultMessage)
        {
            resultMessage = chat.GetScore();
            chat.IsTraningInProcess = false;
            return true;
        }

        public string ReturnText()
        {
            return "Тренировка остановлена!";
        }
    }
}
