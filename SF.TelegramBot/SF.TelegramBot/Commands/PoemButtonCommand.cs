#pragma warning disable CS0618 // Type or member is obsolete
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace SF.TelegramBot.Commands
{
    public class PoemButtonCommand : AbstractCommand, IKeyBoardCommand
    {
        ITelegramBotClient botClient;

        public PoemButtonCommand(ITelegramBotClient botClient)
        {
            this.botClient = botClient;

            CommandText = "/poembuttons";
        }

        public void AddCallBack(Conversation chat)
        {
            this.botClient.OnCallbackQuery -= Bot_Callback;
            this.botClient.OnCallbackQuery += Bot_Callback;
        }

        private async void Bot_Callback(object sender, CallbackQueryEventArgs e)
        {
            string text;

            switch (e.CallbackQuery.Data)
            {
                case "pushkin":
                    text = string.Format("{0}\n{1}\n{2}\n{3}", "<pre>Я помню чудное мгновенье:",
                             "Передо мной явилась ты,",
                             "Как мимолетное виденье,",
                             "Как гений чистой красоты.</pre>"); 
                    break;
                case "esenin":
                    text = string.Format("{0}\n{1}\n{2}", "<pre>Не каждый умеет петь,",
                             "Не каждому дано яблоком",
                             "Падать к чужим ногам.</pre>");
                    break;
                default:
                    text = "...";
                    break;
            }

            
            await botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, text, ParseMode.Html);
            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
            this.botClient.OnCallbackQuery -= Bot_Callback;
        }

        public InlineKeyboardMarkup ReturnKeyBoard()
        {
            var buttonList = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton
                {
                    Text = "Пушкин",
                    CallbackData = "pushkin"
                },

                new InlineKeyboardButton
                {
                    Text = "Есенин",
                    CallbackData = "esenin"
                }
            };

            var keyboard = new InlineKeyboardMarkup(buttonList);

            return keyboard;
        }

        public string InformationalMessage()
        {
            return "Выберите поэта";
        }
    }
}
