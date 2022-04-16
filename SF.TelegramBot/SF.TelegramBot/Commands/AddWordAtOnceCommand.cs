using System.Linq;
using SF.TelegramBot.EnglishTrainer.Model;

namespace SF.TelegramBot.Commands
{
    public class AddWordAtOnceCommand : ChatTextCommandOption, IChatTextCommandWithAction
    {
        public AddWordAtOnceCommand()
        {
            CommandText = "/addwordatonce";         // формат: /addwordatonce рус_слово,eng_word,тема
        }

        public bool DoAction(Conversation chat, out string resultMessage)
        {
            var message = chat.GetLastMessage();
            var text = ClearMessageFromCommand(message);

            if (string.IsNullOrWhiteSpace(text))
            {
                resultMessage = "Отсутствует параметр команды. ";
                return false;
            }

            string[] tokens = text.Split(',');
            if (tokens.Any(u => string.IsNullOrWhiteSpace(u)) || tokens.Length != 3)
            {
                resultMessage = "Одно из полей слова не заполнено. ";
                return false;
            }

            Word word = new Word()
            {
                Russian = tokens[0].Trim(),
                English = tokens[1].Trim(),
                Theme = tokens[2].Trim()
            };

            if (Conversation.dictionary.ContainsKey(word.Russian))
            {
                resultMessage = "Данное слово уже есть в словаре.";
                return false;
            }    

            Conversation.dictionary.Add(word.Russian, word);
            resultMessage = "";
            return true;
        }

        public string ReturnText()
        {
            return "Слово успешно добавлено!";
        }
    }
}
