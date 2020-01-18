using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Test.Core;
using Test.Services;

namespace Test.OrderGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderServices.StartOders();
        }
    }
}
