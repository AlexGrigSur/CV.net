using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPISserver.DataBase;

namespace IPISserver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServerSetUp();
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Function that trying to connect with database. Retry if database unavaliable
        /// </summary>
        private static void ServerSetUp()
        {
            while(true)
            {
                try
                {
                    DBCommands.CreateDataBase();
                    break;
                }
                catch
                {
                    Console.WriteLine("Programm can't connect to db. Retry in 5 seconds");
                    Thread.Sleep(5000);
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
