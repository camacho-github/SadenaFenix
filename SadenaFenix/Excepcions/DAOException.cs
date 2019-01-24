using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Excepcions
{
    [Serializable]
    public class DAOException : Exception
    {
        public int Codigo { get; set; }
        public DAOException() : base() { }
        public DAOException(string message) : base(message) { }
        public DAOException(int codigo, string message) : base(message)
        {
            this.Codigo = codigo;
        }
        public DAOException(string message, Exception innerException) : base(message, innerException) { }
        protected DAOException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


    }
}
