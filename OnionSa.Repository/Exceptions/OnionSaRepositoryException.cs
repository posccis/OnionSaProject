using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSa.Repository.Exceptions
{
    public class OnionSaRepositoryException : Exception
    {
        public OnionSaRepositoryException(string message) : base(message)
        {
            
        }
    }
}
