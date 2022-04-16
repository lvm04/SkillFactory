using System;
using System.Collections.Generic;
using System.Text;

namespace SF.TelegramBot
{
    public class SayHiCommand : AbstractCommand, IChatTextCommand
    {
        public SayHiCommand()
        {
            CommandText = "/saymehi";
        }

        public string ReturnText()
        {
            return "Привет!";
        }

    }
}
