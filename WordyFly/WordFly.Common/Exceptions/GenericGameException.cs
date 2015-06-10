using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Common.Exceptions
{
    public class InsufficientMasterAlphaException : BaseException
    {
        private const string title = "Master Alpha is not Sufficient !";

        public InsufficientMasterAlphaException(string message)
            : base(string.Concat(title, message))
        {
        }

        public InsufficientMasterAlphaException(string message, Exception inner)
            : base(string.Concat(title, message), inner)
        {
        }
    }
}
