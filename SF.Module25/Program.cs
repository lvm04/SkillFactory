using SF.Module25.Model;
using SF.Module25.Repositories;

#region Пользователи
var users = new UserRepository();

Console.WriteLine("Список пользователей:"); 
foreach (var user in users.FindAll())
{
    Console.WriteLine(user);
}

Console.WriteLine("\nПользователь по Id:");
Console.WriteLine(users.FindById(6));

Console.WriteLine("\nУдаление пользователя:");
// users.Delete(10);
// User? user1 = users.FindById(1);
// users.Delete(user1!);

Console.WriteLine("\nСоздание пользователя:");
// User user2 = new User { Name = "Пётр", Email = "petya@mail.ru" };
// users.Create(user2);

Console.WriteLine("\nИзменение имени пользователя:");
// users.UpdateName(4, "Дмитрий");
#endregion

Console.WriteLine("------------------------------------------------\n");

#region Книги

var books = new BookRepository();

Console.WriteLine("Список книг:");
foreach (var book in books.FindAll())
{
    Console.WriteLine(book);
}

Console.WriteLine("\nКнига по Id:");
Console.WriteLine(books.FindById(4));

Console.WriteLine("\nУдаление книги:");
// books.Delete(1);
// Book? book1 = books.FindById(5);
// books.Delete(book1!);

Console.WriteLine("\nСоздание книги:");
// Book book2 = new Book { Name = "Война и мир", Author = "Толстой Л.Н.", Year = 1975, Genre = "Роман" };
// books.Create(book2);

Console.WriteLine("\nИзменение года выпуска книги:");
//books.UpdateYear(4, 2000);

#endregion

Console.WriteLine("------------------------------------------------\n");

Console.WriteLine("\nСписок книг определенного жанра и вышедших между определенными годами:");
foreach (var book in books.GetBooksByGenreAndYears("Детектив", 1970, 1980))
{
    Console.WriteLine(book);
}

Console.WriteLine("\nКоличество книг определенного автора в библиотеке: {0}", books.GetBooksCountByAuthor("Автор-1"));
Console.WriteLine("\nКоличество книг определенного жанра в библиотеке: {0}", books.GetBooksCountByGenre("Детектив"));
Console.WriteLine("\nЕсть ли книга определенного автора и с определенным названием в библиотеке: {0}", 
    books.IsTheBookAvailableByAuthor("Автор-3", "Книга-4"));
Console.WriteLine("\nЕсть ли определенная книга на руках у пользователя: {0}", books.IsTheBookIssued(9));
Console.WriteLine("\nКоличество книг на руках у пользователя: {0}", users.QtyBooksHasUser(2));
Console.WriteLine("\nПоследняя книга: {0}", books.GetLastBook());

Console.WriteLine("\nСписок книг с сортировкой по названию:");
foreach (var book in books.FindAll("name"))
{
    Console.WriteLine(book);
}

Console.WriteLine("\nСписок книг с сортировкой по убыванию года выпуска:");
foreach (var book in books.FindAll("year desc"))
{
    Console.WriteLine(book);
}

