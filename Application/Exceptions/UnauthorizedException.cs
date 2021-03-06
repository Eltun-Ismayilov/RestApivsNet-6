using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string name, object key)
            : base($"User \"{name}\" not authorized.") { }
    }
}
