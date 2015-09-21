namespace Host
{
    using Microsoft.Owin.Hosting;
    using System;
    using System.Diagnostics;

    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO use params for URL
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            const string url = "http://localhost:8085";

            using (WebApp.Start(url))
            {
                Process.Start(url);
                Console.ReadKey();
            }
        }
    }
}