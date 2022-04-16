using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;
using SF.TelegramBot.EnglishTrainer.Model;
using Newtonsoft.Json;

namespace SF.TelegramBot
{
    /// <summary>
    /// Класс для хранения сообщиний чата и русско-англ. словаря
    /// </summary>
    public class Conversation
    {
        private Chat telegramChat;
        private List<Message> telegramMessages;
        static public readonly Dictionary<string, Word> dictionary;            // глобальный словарь
        static public readonly string dictPath;

        private List<Word> traningWords;                                     // список слов для режима тренировки 
        public bool IsAddingInProcess;
        private bool _isTraningInProcess;
        private int totalQuestions;
        private int correctAnswers;

        public bool IsTraningInProcess
        {
            get { return _isTraningInProcess; }
            set 
            {
                _isTraningInProcess = value;
                if (value == false)
                {
                    traningWords = null;
                    totalQuestions = 0;
                    correctAnswers = 0;
                }
                else
                {
                    if (dictionary != null && dictionary.Count > 1)
                    {
                        traningWords = dictionary.Values.ToList();
                        traningWords.Shuffle();
                    }
                }
            }
        }

        static Conversation()
        {
            dictPath = "dictionary.json";
            dictionary = new Dictionary<string, Word>();
            try
            {
                if (System.IO.File.Exists(dictPath))
                    dictionary = JsonConvert.DeserializeObject<Dictionary<string, Word>>(System.IO.File.ReadAllText(dictPath));
                else
                    dictionary = InitDict();
            }
            catch (Exception)
            {
                Console.WriteLine("*ERROR* Не удалось загрузить словарь из файла");
            }
        }

        public Conversation(Chat chat)
        {
            telegramChat = chat;
            telegramMessages = new List<Message>();
        }

        public void AddMessage(Message message)
        {
            telegramMessages.Add(message);
        }

        public void AddWord(string key, Word word)
        {
            dictionary.Add(key, word);
        }

        public void ClearHistory()
        {
            telegramMessages.Clear();
        }

        public List<string> GetTextMessages()
        {
            var textMessages = new List<string>();

            foreach(var message in telegramMessages)
            {
                if (message.Text != null)
                {
                    textMessages.Add(message.Text);
                }
            }

            return textMessages;
        }

        public long GetId() => telegramChat.Id;

        public string GetLastMessage() => telegramMessages[^1].Text;

        // Новый метод выбора слова
        public string GetTrainingWord(TrainingType type)
        {
            if (traningWords == null || traningWords.Count == 0)
                return "/stop";

            Word randomword = traningWords.Last();
            traningWords.RemoveAt(traningWords.Count - 1);
            string text = "";

            switch (type)
            {
                case TrainingType.EngToRus:
                    text = randomword.English;
                    break;

                case TrainingType.RusToEng:
                    text = randomword.Russian;
                    break;
            }
            
            return text;
        }

        public string GetTrainingWord_(TrainingType type)
        {
            var rand = new Random();
            var item = rand.Next(0, dictionary.Count);

            var randomword = dictionary.Values.AsEnumerable().ElementAt(item);

            var text = string.Empty;

            switch (type)
            {
                case TrainingType.EngToRus:
                    text =  randomword.English;
                    break;

                case TrainingType.RusToEng:
                    text = randomword.Russian;
                    break;
            }

            return text;
        }

        public bool CheckWord(TrainingType type, string word, string answer)
        {
            Word control;
            var result = false;
            totalQuestions++;

            switch (type)
            {
                case TrainingType.EngToRus:
                    control = dictionary.Values.FirstOrDefault(x => x.English == word);
                    result = control.Russian == answer;
                    break;

                case TrainingType.RusToEng:
                    control = dictionary[word];
                    result = control.English == answer;
                    break;
            }

            if (result) correctAnswers++;
            return result; 
        }

        public string GetScore()
        {
            int percent = totalQuestions > 0 ? Convert.ToInt32((double)correctAnswers * 100 / (double)totalQuestions) : 0;
            return $"Количество правильных ответов: <b>{correctAnswers}</b> из <b>{totalQuestions} ({percent}%)</b>. ";
        }

        // Словарь по умолчанию для тестирования режима тренировки
        static Dictionary<string, Word> InitDict()
        {
            Dictionary<string, Word> dictionary = new Dictionary<string, Word>()
            {
                { "стол", new Word() { English = "table", Russian = "стол", Theme = "мебель" } },
                { "стул", new Word() { English = "chair", Russian = "стул", Theme = "мебель" } },
                { "кот", new Word() { English = "cat", Russian = "кот", Theme = "животные" } },
                { "собака", new Word() { English = "dog", Russian = "собака", Theme = "животные" } },
                { "курица", new Word() { English = "hen", Russian = "курица", Theme = "животные" } },
                { "яблоко", new Word() { English = "apple", Russian = "яблоко", Theme = "фрукты" } },
                { "апельсин", new Word() { English = "orange", Russian = "апельсин", Theme = "фрукты" } },
                { "человек", new Word() { English = "human", Russian = "человек", Theme = "люди" } },
                { "мальчик", new Word() { English = "boy", Russian = "мальчик", Theme = "люди" } },
                { "девочка", new Word() { English = "girl", Russian = "девочка", Theme = "люди" } }
            };

            return dictionary;
        }

    }
}
