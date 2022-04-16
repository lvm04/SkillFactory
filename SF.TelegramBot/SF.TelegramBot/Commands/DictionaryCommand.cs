using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SF.TelegramBot
{
    public class DictionaryCommand : AbstractCommand, IChatTextCommand
    {
        public DictionaryCommand()
        {
            CommandText = "/dictionary";
        }

        public string ReturnText()
        {
            
            StringBuilder sb = new StringBuilder(1000);

            // Список слов с разбивкой по группам
            var groupWords = from word in Conversation.dictionary.Values
                             group word by word.Theme;
            foreach (var grp in groupWords.OrderBy(g => g.Key))
            {
                sb.Append($"<u>{grp.Key}</u>\n");
                foreach (var word in grp.OrderBy(w => w.Russian))
                {
                    sb.Append($"<pre>  Рус: {word.Russian, 10}  Eng: {word.English, 10}</pre>");
                }

            }

            return sb.ToString();
        }

    }
}
