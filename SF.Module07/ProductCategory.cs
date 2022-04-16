using System;

namespace SF.Module7
{
    // Категория товара
    class ProductCategory
    {
        private int _id;
        public int Id { get { return _id; } }
        public string Name { get; }
        public ProductCategory(int id, string name)
        {
            _id = id;
            Name = name;
        }
    }

    class Categories
    {
        private static ProductCategory[] categoryList;
        private static Categories _categories = new Categories();
        public static Categories Get
        {
            get { return _categories; }
        }
        private Categories()
        {
            categoryList = new ProductCategory[]
            {
                new ProductCategory(1, "Смартфон"),
                new ProductCategory(2, "Планшет"),
                new ProductCategory(3, "Наушники")
            };
        }

        public ProductCategory this[int index]
        {
            get
            {
                if (index < 0 || index > categoryList.Length - 1)
                    return new ProductCategory(-1, "Неизвестная категория товара");
                else
                    return categoryList[index];

            }
        }

        public ProductCategory this[string name]
        {
            get
            {
                foreach (var item in categoryList)
                {
                    if (item.Name == name)
                        return item;
                }

                return new ProductCategory(-1, "Неизвестная категория товара");
            }
        }

        public void Display()
        {
            foreach (var item in categoryList)
            {
                Console.WriteLine($"[{item.Id}] - {item.Name}");
            }
        }
    }
}