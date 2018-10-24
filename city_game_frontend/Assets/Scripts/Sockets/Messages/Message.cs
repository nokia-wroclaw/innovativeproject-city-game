using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Messages
{
    public enum Type
    {
        auth,
        chunk
    }

    class Message
    {
        public Type message_type;
        public string message;
    }
}
