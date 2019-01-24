using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Excepcions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public int Codigo { get; set; }
        public BusinessException() : base() { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(int codigo, string message) : base(message)
        {
            this.Codigo = codigo;
        }
        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
        protected BusinessException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


    }
}
