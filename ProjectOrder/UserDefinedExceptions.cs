using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrder
{
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
    class EmptyInputFileException : Exception
    {
        public EmptyInputFileException(string msg) : base(msg)
        {

        }
    }
}
