using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Excepcions
{
    [Serializable]
    public class EmptyDataException : DAOException
    {
        public EmptyDataException() : base() { }
        public EmptyDataException(string message) : base(message) { }
        public EmptyDataException(string message, Exception innerException) : base(message, innerException) { }
        protected EmptyDataException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
