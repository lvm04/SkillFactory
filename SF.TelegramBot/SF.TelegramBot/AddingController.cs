using System;
using System.Collections.Generic;
using System.Text;
using SF.TelegramBot.EnglishTrainer.Model;

namespace SF.TelegramBot
{
    public class AddingController
    {
        private readonly Dictionary<long, AddingState> ChatAdding;

        public AddingController()
        {
            ChatAdding = new Dictionary<long, AddingState>();
        }

        public void AddFirstState(Conversation chat)
        {
            ChatAdding.Add(chat.GetId(), AddingState.Russian);
        }

        public void NextStage(string message, Conversation chat)
        {
            var currentstate = ChatAdding[chat.GetId()];
            ChatAdding[chat.GetId()] = currentstate + 1;

            if (ChatAdding[chat.GetId()] == AddingState.Finish)
            {
                Reset(chat);
            }
        }

        public AddingState GetStage(Conversation chat)
        {
            return ChatAdding[chat.GetId()];
        }

        public void Reset(Conversation chat)
        {
            chat.IsAddingInProcess = false;
            ChatAdding.Remove(chat.GetId());
        }

    }
}
