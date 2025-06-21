using PizzeriaAppTest.Utilities;
using PizzeriaAppTest.Data;

namespace PizzeriaAppTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitializeApp();
            Console.WriteLine("Hello, World!");
        }

        static void InitializeApp()
        {
            Console.WriteLine("Application starting!...");
            SeedData.Load();
        }
    }
}
