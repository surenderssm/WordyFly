using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Common.Exceptions
{
    public class CreateGameFailedException : BaseException
    {
        private const string title = "CreateGameFailedException";

        public CreateGameFailedException(string message)
            : base(string.Concat(title, message))
        {
        }

        public CreateGameFailedException(string message, Exception inner)
            : base(string.Concat(title, message), inner)
        {
        }
    }
}
