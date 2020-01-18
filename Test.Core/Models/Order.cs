using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core
{
    [Serializable()]
    public class Order : BaseEntity, ISerializable
    {
        public bool Status { get; set; }
        public Order()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Status", Status);
        }
        public Order(SerializationInfo info, StreamingContext context)
        {
            Status = (bool)info.GetValue("Status", typeof(bool));
        }
        public Order(bool status)
        {
            this.Status = status;
        } 
    }
}
