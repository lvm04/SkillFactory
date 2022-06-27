using SF.Module35.Models;

namespace SF.Module35.Data
{
    public class GenetateUsers
    {
        private readonly string[] maleNames = new string[] { "Александр", "Борис", "Василий", "Игорь", "Даниил", "Сергей", "Евгений", "Алексей", "Геогрий", "Валентин" };
        private readonly string[] femaleNames = new string[] { "Анна", "Мария", "Станислава", "Елена", "Галина", "Зинаида", "Ольга" };
        private readonly string[] lastNames = new string[] { "Тестов", "Титов", "Потапов", "Джабаев", "Иванов", "Петров", "Скворцов", "Соловьев", "Кукушкин", "Демчук" };

        HttpClient client = new HttpClient();                    // для загр. фотографий
        private readonly IWebHostEnvironment _env;

        public GenetateUsers(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<List<User>> PopulateAsync(int count)
        {
            var users = new List<User>();
            for (int i = 1; i < count; i++)
            {
                string firstName;
                var rand = new Random();

                var male = rand.Next(0, 2) == 1;

                var lastName = lastNames[rand.Next(0, lastNames.Length - 1)];
                if (male)
                {
                    firstName = maleNames[rand.Next(0, maleNames.Length - 1)];
                }
                else
                {
                    lastName = lastName + "a";
                    firstName = femaleNames[rand.Next(0, femaleNames.Length - 1)];
                }

                var item = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = DateTime.Now.AddDays(-rand.Next(1, (DateTime.Now - DateTime.Now.AddYears(-25)).Days)),
                    Email = "test" + rand.Next(0, 1204) + "@test.com",
                };

                item.UserName = item.Email;
                item.Image = await LoadPhoto("https://thispersondoesnotexist.com/image");

                users.Add(item);
            }

            return users;
        }

        /// <summary>
        /// Загружает фото в каталог
        /// </summary>
        /// <param name="url">Адрес картинки</param>
        /// <returns>Имя файла</returns>
        async private Task<string> LoadPhoto(string url)
        {
            string fileName = "";
            try
            {
                Thread.Sleep(500);
                HttpResponseMessage response = await client.GetAsync(url);

                fileName = $"/img/{Guid.NewGuid()}.jpg";
                byte[] responseBody = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(_env.WebRootPath + fileName, responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message: {0} ", e.Message);
            }

            return fileName;
        }
    }
}
