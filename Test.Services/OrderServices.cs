using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Test.Core;
using Test.DataAccess.Sql;

namespace Test.Services
{
    public class OrderServices
    {
        private static bool Status;
        private static DataContext context = new DataContext();

        public static void StartOders()
        {
            Console.WriteLine("To stop Writing the files out press  enter");
                var timer = new Timer(SerializeOrders, null, 0, 5000);
                Console.ReadLine();
            
        }

        public static void SerializeOrders(object o)
        {
            if (Status == true)
            {
                Status = false;
            }
            else 
            {
                Status = true;
            }
            
            Order order = new Order(Status);
            XmlSerializer serializer = new XmlSerializer(typeof(Order));
            var Unique = Guid.NewGuid().ToString();
            using (TextWriter tw = new StreamWriter(@"C:\Users\aman\source\repos\Test\Orders\Order." + Unique + ".xml"))
            {
                serializer.Serialize(tw, order);
            }
        }
        public static void DeSerializeAllOrders()
        {
            List<string> files = Directory.GetFiles(@"C:\Users\aman\source\repos\Test\Orders").ToList();
            foreach (var file in files)
            {
                var order = DeSerializeOrders(file);
                order.Id = Guid.NewGuid().ToString();
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }

        public static  Order DeSerializeOrders(string path)
        {

            XmlSerializer deserializer = new XmlSerializer(typeof(Order));
            TextReader reader = new StreamReader(path);
            var order = (Order)deserializer.Deserialize(reader);
            order.Filename = path;
            order.CreatedAt = DateTime.Now;
            reader.Close();
            return order;
        }

    }
}
