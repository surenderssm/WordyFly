using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Common.Exceptions
{
    public class GenericGameException : BaseException
    {
        private const string title = "GenericGameException";

        public GenericGameException(string message)
            : base(string.Concat(title, message))
        {
        }

        public GenericGameException(string message, Exception inner)
            : base(string.Concat(title, message), inner)
        {
        }
    }
}
