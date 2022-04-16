using SF.TelegramBot.Commands;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace SF.TelegramBot
{
    public class CommandParser
    {
        private List<IChatCommand> commands;            // список допустимых команд
        private AddingController addingController;      // объект хранящий стадию добавления слова

        public CommandParser()
        {
            commands = new List<IChatCommand>();
            addingController = new AddingController();
        }

        public void AddCommand(IChatCommand chatCommand)
        {
            commands.Add(chatCommand);
        }

        public bool IsMessageCommand(string message)
        {
            return commands.Exists(x => x.CheckMessage(message));
        }

        public bool IsTextCommand(string message)
        {
            var command = commands.Find(x => x.CheckMessage(message));
            return command is IChatTextCommand;
        }

        public bool IsButtonCommand(string message)
        {
            var command = commands.Find(x => x.CheckMessage(message));
            return command is IKeyBoardCommand;
        }

        public bool IsAddingCommand(string message)
        {
            var command = commands.Find(x => x.CheckMessage(message));
            return command is AddWordCommand;
        }

        // Текстовый ответ
        public string GetMessageText(string message, Conversation chat)
        {
            var command = commands.Find(x => x.CheckMessage(message)) as IChatTextCommand;
            string resultText = "";

            if (command is IChatTextCommandWithAction)
            {
                if (!(command as IChatTextCommandWithAction).DoAction(chat, out string resultMessage))
                {
                    return $"{resultMessage} Ошибка выполнения команды!";
                }
                else
                    resultText = resultMessage;
            }

            return resultText + command.ReturnText();
        }

        // Ответ в виде кнопок
        public string GetInformationalMessage(string message)
        {
            var command = commands.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;
            return command.InformationalMessage();
        }

        public InlineKeyboardMarkup GetKeyBoard(string message)
        {
            var command = commands.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;
            return command.ReturnKeyBoard();
        }

        public void AddCallback(string message, Conversation chat)
        {
            var command = commands.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;
            command.AddCallBack(chat);
        }

        // Добавление слова по шагам
        public void StartAddingWord(string message, Conversation chat)
        {
            var command = commands.Find(x => x.CheckMessage(message)) as AddWordCommand;

            addingController.AddFirstState(chat);
            command.StartProcessAsync(chat, addingController);
        }

        public void NextStage(string message, Conversation chat)
        {
            var command = commands.Find(x => x is AddWordCommand) as AddWordCommand;

            if (message == "/stop")
            {
                command.Abort(chat);
            }
            else if (Conversation.dictionary.ContainsKey(message))
            {
                command.Abort(chat, "Данное слово уже есть в словаре. Введите команду /addword заново. ");
            }
            else
            {
                command.DoForStageAsync(addingController.GetStage(chat), chat, message);
                addingController.NextStage(message, chat);
            }
        }

        // Тренировка
        public void ContinueTraining(string message, Conversation chat)
        {
            var command = commands.Find(x => x is TrainingCommand) as TrainingCommand;

            command.NextStepAsync(chat, message);
        }

    }
}
