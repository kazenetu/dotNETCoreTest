using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding =System.Text.Encoding.UTF8;

            var hellow = new Hellow("C#");
            Console.WriteLine(hellow.GetFormatName());
        }
    }
}
