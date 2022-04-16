#pragma warning disable CS0618 // Type or member is obsolete
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SF.TelegramBot
{
    /// <summary>
    /// Хранит список чатов. Вызывает у мессенджера команду формирующую ответ
    /// </summary>
    public class BotMessageLogic
    {
        private Messenger messenger;
        private Dictionary<long, Conversation> chatList;

        public BotMessageLogic(ITelegramBotClient botClient)
        {
            messenger = new Messenger(botClient);
            chatList = new Dictionary<long, Conversation>();
        }

        public async Task Response(MessageEventArgs e)
        {
            var Id = e.Message.Chat.Id;

            if (!chatList.ContainsKey(Id))
            {
                var newchat = new Conversation(e.Message.Chat);

                chatList.Add(Id, newchat);
            }

            var chat = chatList[Id];

            chat.AddMessage(e.Message);

            await SendMessage(chat);

        }

        private async Task SendMessage(Conversation chat)
        {
            await messenger.MakeAnswer(chat);
        }
    }
}
