using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Services;

namespace Test.OrderConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderServices.DeSerializeAllOrders();
        }
    }
}
