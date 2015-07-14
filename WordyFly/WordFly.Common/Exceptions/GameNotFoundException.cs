using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Common.Exceptions
{
    public class GameNotFoundException : BaseException
    {
        private const string title = "GameNotFoundException";

        public GameNotFoundException(string message)
            : base(string.Concat(title, message))
        {
        }

        public GameNotFoundException(string message, Exception inner)
            : base(string.Concat(title, message), inner)
        {
        }
    }
}
