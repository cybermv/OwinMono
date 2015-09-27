namespace Host
{
    using Common;
    using Microsoft.Owin.Hosting;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleLogger logger = ConsoleLogger.Instance;
            string url = "http://localhost:80";

            if (args.Length == 1)
            {
                url = args[0];
            }

            logger.Write("Starting self-hosted application at '{0}'...", url);

            using (WebApp.Start<Startup>(url))
            {
                logger.Write("Start successful! Press X twice to stop application.");

                while (true)
                {
                    ConsoleKeyInfo readKey = Console.ReadKey(true);

                    if (readKey.Key == ConsoleKey.X)
                    {
                        logger.Write("Press X again to stop the application.", ConsoleColor.Red);
                        ConsoleKeyInfo secondKey = Console.ReadKey(true);

                        if (secondKey.Key == ConsoleKey.X)
                        {
                            break;
                        }
                    }
                }
            }

            logger.Write("Application stopped.");
        }
    }
}