using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrder
{
    class CircularDependencyException : Exception
    {
        public CircularDependencyException(string msg) : base(msg)
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

    class SelfDependencyException : Exception
    {
        public SelfDependencyException(string msg) : base(msg)
        {

        }
    }

    class SingleDependencyException : Exception
    {
        public SingleDependencyException(string msg) : base(msg)
        {

        }
    }
}
