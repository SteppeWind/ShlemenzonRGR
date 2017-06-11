using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsForum.Model.Exceptions
{
    class IsBannedUserException : Exception
    {
        public override string Message { get; }

        public IsBannedUserException() : base() { }

        public IsBannedUserException(string message) : base(message)
        {
            Message = message;
        } 
     }
}