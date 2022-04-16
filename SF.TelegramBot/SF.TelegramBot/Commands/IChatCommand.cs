using System;
using System.Collections.Generic;
using System.Text;
using SF.TelegramBot.EnglishTrainer.Model;

namespace SF.TelegramBot
{
    public interface IChatCommand
    {
        bool CheckMessage(string message);
    }
}
