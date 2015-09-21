namespace Common
{
    using System;
    using System.Threading.Tasks;

    public class ConsoleLogger
    {
        private static ConsoleLogger _instance;
        private static readonly object Lock = new object();

        private ConsoleColor _defaultForeColor = ConsoleColor.White;
        private ConsoleColor _defaultBackColor = ConsoleColor.Black;

        private ConsoleLogger()
        {
            Console.ForegroundColor = this._defaultForeColor;
            Console.BackgroundColor = this._defaultBackColor;
        }

        public static ConsoleLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConsoleLogger();
                        }
                    }
                }
                return _instance;
            }
        }

        public void SetDefaultColors(ConsoleColor fore, ConsoleColor back)
        {
            lock (Lock)
            {
                this._defaultForeColor = fore;
                this._defaultBackColor = back;
            }
        }

        public void Write(string line)
        {
            this.Write(line, this._defaultForeColor);
        }

        public void Write(string format, params object[] args)
        {
            this.Write(format, this._defaultForeColor, args);
        }

        public void Write(string line, ConsoleColor color)
        {
            this.Write(line, color, null);
        }

        public void Write(string format, ConsoleColor color, params object[] args)
        {
            lock (Lock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(format, args);
                Console.ForegroundColor = this._defaultForeColor;
            }
        }
    }
}