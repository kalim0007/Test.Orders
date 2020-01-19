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
            Console.WriteLine("Shifting Orders To Database");
            List<string> files = Directory.GetFiles(@"C:\Users\aman\source\repos\Test\Orders").ToList();
            foreach (var filePath in files)
            {
                FileInfo file = new FileInfo(filePath);
                var orderInDataBase = context.Orders.FirstOrDefault(o => o.Filename == file.Name);
                if (file.Extension==".xml")
                {
                    if (orderInDataBase == null)
                    {
                        var order = DeSerializeOrders(file, filePath);
                        order.Id = Guid.NewGuid().ToString();
                        context.Orders.Add(order);
                        context.SaveChanges();
                    }
                }
            }
        }

        public static  Order DeSerializeOrders(FileInfo file,string filePath)
        {

            XmlSerializer deserializer = new XmlSerializer(typeof(Order));
            TextReader reader = new StreamReader(filePath);
            var order = (Order)deserializer.Deserialize(reader);
            order.Filename = file.Name;
            order.FileContent = System.IO.File.ReadAllText(filePath);
            order.CreatedAt = DateTime.Now;
            reader.Close();
            return order;
        }

    }
}
