#pragma warning disable CS0618 // Type or member is obsolete
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using SF.TelegramBot.EnglishTrainer.Model;

namespace SF.TelegramBot.Commands
{
    public class TrainingCommand : AbstractCommand, IKeyBoardCommand
    {
        private ITelegramBotClient botClient;
        private Dictionary<long, TrainingType> training;
        private Dictionary<long, Conversation> trainingChats;
        private Dictionary<long, string> activeWord;

        public TrainingCommand(ITelegramBotClient botClient)
        {
            CommandText = "/training";

            this.botClient = botClient;

            training = new Dictionary<long, TrainingType>();
            trainingChats = new Dictionary<long, Conversation>();
            activeWord = new Dictionary<long, string>();
        }

        public InlineKeyboardMarkup ReturnKeyBoard()
        {
            var buttonList = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton
                {
                    Text = "С русского на английский",
                    CallbackData = "rustoeng"
                },

                new InlineKeyboardButton
                {
                    Text = "С английского на русский",
                    CallbackData = "engtorus"
                }
            };

            var keyboard = new InlineKeyboardMarkup(buttonList);

            return keyboard;
        }

        public string InformationalMessage()
        {
            return "Выберите тип тренировки. Для окончания тренировки введите команду /stop";
        }

        public void AddCallBack(Conversation chat)
        {
            trainingChats[chat.GetId()] = chat;

            this.botClient.OnCallbackQuery -= Bot_Callback;
            this.botClient.OnCallbackQuery += Bot_Callback;
        }

        private async void Bot_Callback(object sender, CallbackQueryEventArgs e)
        {
            string text = "";
            var id = e.CallbackQuery.Message.Chat.Id;
            var chat = trainingChats[id];
            chat.IsTraningInProcess = true;

            switch (e.CallbackQuery.Data)
            {
                case "rustoeng":
                    training[id] = TrainingType.RusToEng;
                    text =  chat.GetTrainingWord(TrainingType.RusToEng);
                    break;
                case "engtorus":
                    training[id] = TrainingType.EngToRus;
                    text = chat.GetTrainingWord(TrainingType.EngToRus);
                    break;
                default:
                    text = "no word";
                    break;
            }

            activeWord[id] = text;

            await botClient.SendTextMessageAsync(id, text);
            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);       // ответ серверу, что нажатие обработано
            this.botClient.OnCallbackQuery -= Bot_Callback;
        }

        public async void NextStepAsync(Conversation chat, string message)
        {

            var type = training[chat.GetId()];
            string newword = chat.GetTrainingWord(type);
            var text = "";

            if (newword == "/stop")
            {
                text = $"Вам были показаны все имеющиеся слова. Тренировка окончена! {chat.GetScore()}";
                chat.IsTraningInProcess = false;
                await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text, parseMode: ParseMode.Html);
                return;
            }

            var word = activeWord[chat.GetId()];
            var check = chat.CheckWord(type, word, message);
           
            if (check)
            {
                text = "Правильно!";
            }
            else
            {
                text = "Неправильно!";
            }
            
            text += $" Следующее слово: <i>{newword}</i>";
            
            activeWord[chat.GetId()] = newword;

            await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text, parseMode: ParseMode.Html);
        }
    }
}
