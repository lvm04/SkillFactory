using System;

namespace Module16
{
    public class Calculator
    {

        public int Additional(int a, int b)
        {
            return a + b;
        }

        public int Subtraction(int a, int b)
        {
            return a - b;
        }

        public int Miltiplication(int a, int b)
        {
            return a * b;
        }

        public int Division(int a, int b)
        {
            if (b == 0)
                throw new ArgumentException();
            return a / b;
        }
    }
}

