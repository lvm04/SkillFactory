using System;

namespace SF.Module7
{
    // Сотрудник
    public class Employee : Person
    {
        public string Position { get;  }         // Должность
        public DateTime BirthDate { get; }
        private int _salary;

        public Employee()               // для сериализации в XML
        {
            
        }
        public Employee(int id, string name, string position, DateTime birthDate, int salary) : base(id, name)
        {
            Position = position;
            BirthDate = birthDate;
        }

        public Employee(int id, string name, string position, 
                    DateTime birthDate, string address, string phone, int salary) : 
                        this(id, name, position, birthDate, salary)
        {
            Address = address;
            Phone = phone;
            _salary = salary;
        }

        public int Salary { get { return _salary; } }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Дата рожд.: {0}", BirthDate.ToShortDateString());
            Console.WriteLine("Должность : {0}", Position);
        }

    }
    
}