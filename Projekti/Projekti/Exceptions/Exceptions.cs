using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Projekti
{


    //If the racer goes impossibly fast on the track
    [Serializable()]
    public class TooFastException : Exception
    {
        public TooFastException() : base(String.Format("Too Fast")) { }
        
        public TooFastException(string message) : base(String.Format(message)) { }

        public TooFastException(string message, Exception inner) : base(String.Format("Too Fast", message, inner)) { }

        protected TooFastException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
