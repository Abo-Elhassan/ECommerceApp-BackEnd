using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    //why we use enum not class?? because enum will give 0 to pending & 1 to pay recived and so on
    public enum OrderStatus
    {
        //3 status 
        // using "enummember" attribute to return a text not a number 

        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "PaymentRecived")]

        PaymentRecived,

        [EnumMember(Value = "PaymentFailed")]

        PaymentFailed

    }
}
