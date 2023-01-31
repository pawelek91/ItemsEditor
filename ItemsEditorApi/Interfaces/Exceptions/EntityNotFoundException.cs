using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {

        }
        public EntityNotFoundException(string code) : base($"Entity with code {code} does not exists")
        {

        }
    }
}
