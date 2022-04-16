using System;
using System.Collections.Generic;
using System.Text;

namespace SF.TelegramBot.Commands
{
    interface IChatTextCommandWithAction: IChatTextCommand
    {
        bool DoAction(Conversation chat, out string resultMessage);
    }
}
