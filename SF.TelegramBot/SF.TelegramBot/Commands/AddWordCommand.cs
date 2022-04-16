using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using SF.TelegramBot.EnglishTrainer.Model;

namespace SF.TelegramBot.Commands
{
    public class AddWordCommand : AbstractCommand
    {

        private readonly ITelegramBotClient botClient;
        private readonly Dictionary<long, Word> Buffer;
        private AddingController addingController;

        public AddWordCommand(ITelegramBotClient botClient)
        {
            CommandText = "/addword";
            this.botClient = botClient;
           
            Buffer = new Dictionary<long, Word>();
        }

        public async void StartProcessAsync(Conversation chat, AddingController addingController)
        {
            Buffer.Add(chat.GetId(), new Word());
            this.addingController = addingController;
            var text = "Введите русское значение слова";
            await SendCommandText(text, chat.GetId());
        }

        public async void DoForStageAsync(AddingState addingState, Conversation chat, string message)
        {
            var word = Buffer[chat.GetId()];
            var text = "";

            switch (addingState)
            {
                case AddingState.Russian:
                    //if (Conversation.dictionary.ContainsKey(message))
                    //{
                    //    Abort(chat, "Данное слово уже есть в словаре. Введите команду /addword заново. ");
                    //    return;
                    //}
                    word.Russian = message;

                    text = "Введите английское значение слова";
                    break;

                case AddingState.English:
                    word.English = message;

                    text = "Введите тематику";
                    break;

                case AddingState.Theme:
                    word.Theme = message;
                    text = "Успешно! Слово " + word.English + " добавлено в словарь. ";

                    Conversation.dictionary.Add(word.Russian, word);

                    Buffer.Remove(chat.GetId());
                    break;
            }

            await SendCommandText(text, chat.GetId());
        }

        public async void Abort(Conversation chat, string resultMessage = "")
        {
            string text = resultMessage != "" ? resultMessage : "Процесс добавления слова прерван.";

            Buffer.Remove(chat.GetId());
            addingController.Reset(chat);
            await SendCommandText(text, chat.GetId());
        }

        private async Task SendCommandText(string text, long chat)
        {
            await botClient.SendTextMessageAsync(chatId: chat, text: text, parseMode: ParseMode.Html);
        }

    }
}
