using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrder
{
    //class UserDefinedExceptions : Exception
    //{
    //    public UserDefinedExceptions(string msg):base(msg)
    //    {
            
    //    }
    //}

    class NotResolvedException : Exception
    {
        public NotResolvedException(string msg) : base(msg)
        {

        }
    }

    class InvalidInputFormatException : Exception
    {
        public InvalidInputFormatException(string msg) : base(msg)
        {

        }
    }
}
