using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NavProject_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server().Start();
        }
    }
}
