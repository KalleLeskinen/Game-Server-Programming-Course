using System;

namespace Assignment1.RealTimeBikes
{
    [Serializable]
    class NotFoundException : Exception
    {

        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
