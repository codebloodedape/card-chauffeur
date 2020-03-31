using System;

namespace Dummy
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int rInt = r.Next(0, 100);

            for (int i = 0; i < 13; i++)
            {
                Console.WriteLine(r.Next(1, 13));
            }
        }
    }
}
