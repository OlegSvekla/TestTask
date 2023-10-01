using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.BL.Exceptions
{
    public sealed class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string message) : base(message)
        {
        }
    }
}