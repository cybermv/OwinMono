namespace Host
{
    using Common;
    using Microsoft.Owin;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class ConsoleLoggingMiddleware : OwinMiddleware
    {
        private const string ReqIdKey = "test.ReqId";
        private long _requestCounter;
        private static readonly object Lock = new object();
        private readonly Stopwatch _stopwatch;
        private readonly ConsoleLogger _logger;

        public ConsoleLoggingMiddleware(OwinMiddleware next)
            : base(next)
        {
            this._stopwatch = new Stopwatch();
            this._logger = ConsoleLogger.Instance;
        }

        public async override Task Invoke(IOwinContext context)
        {
            lock (Lock)
            {
                this._requestCounter++;
                context.Environment[ReqIdKey] = this._requestCounter;
            }

            this._logger.Write("--> Req.Id {0} - {1}: {2}", ConsoleColor.Gray,
                context.Environment[ReqIdKey],
                context.Request.Method,
                Uri.UnescapeDataString(context.Request.Path + context.Request.QueryString));

            this._stopwatch.Restart();
            await Next.Invoke(context);
            this._stopwatch.Stop();

            long requestDuration = this._stopwatch.ElapsedMilliseconds;

            ConsoleColor responseColor;
            if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 400)
            {
                responseColor = ConsoleColor.Green;
            }
            else if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 500)
            {
                responseColor = ConsoleColor.Yellow;
            }
            else
            {
                responseColor = ConsoleColor.Red;
            }

            this._logger.Write("<-- Responded to Req.Id {0} with {1} - {2}, duration: {3} ms", responseColor,
                context.Environment[ReqIdKey],
                context.Response.StatusCode,
                context.Response.ReasonPhrase,
                requestDuration);
        }
    }
}