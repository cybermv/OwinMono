namespace Host
{
    using Common;
    using Microsoft.Owin.Hosting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using WebAPI.DAL;
    using WebAPI.DAL.Entities;

    public class Program
    {
        private static Random Rand = new Random();

        public static void Main(string[] args)
        {
            const string url = "http://localhost:8085";

            using (WebApp.Start(url))
            {
                Process.Start(url);
                Console.ReadKey();
            }

            //GroceriesContext groceriesContext = new GroceriesContext();

            //List<Fruit> fruits = groceriesContext.Fruits.ToList();

            //for (int i = 0; i < 25; i++)
            //{
            //    Thread thread = new Thread(LogSomethingRandom);
            //    thread.Name = i.ToString();
            //    thread.Start();
            //}

            Console.ReadKey();
        }

        private static void LogSomethingRandom()
        {
            int i = 5;
            while (i-- > 0)
            {
                ConsoleColor color = (ConsoleColor)Rand.Next(1, 16);
                ConsoleLogger.Instance.Write("Thread {0} writing for the {1} time using color {2}!", color,
                    Thread.CurrentThread.Name, i, color);
                Thread.Sleep(50);
            }
        }
    }
}