#pragma warning disable CS0618 // Type or member is obsolete
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SF.TelegramBot
{
    /// <summary>
    /// Класс для управления ботом
    /// </summary>
    class BotWorker
    {
        private ITelegramBotClient botClient;
        private BotMessageLogic logic;

        public void Inizalize()
        {
            botClient = new TelegramBotClient(BotCredentials.BotToken);
            var me = botClient.GetMeAsync().Result;
            if (me != null)
                Console.WriteLine("Telegram-бот \"{0}\" запущен.", me.FirstName);
            logic = new BotMessageLogic(botClient);
        }

        public void Start()
        {
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
        }

        public void Stop()
        {
            botClient.StopReceiving();

            if (Conversation.dictionary.Count > 0)
            {
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(Conversation.dictionary, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(Conversation.dictPath, output);
            }
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message != null)
            {
                await logic.Response(e);
            }
        }
    }
}
