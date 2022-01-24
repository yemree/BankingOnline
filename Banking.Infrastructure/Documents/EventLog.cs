using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Infrastructure.Documents
{
    public class EventLog : Document
    {
        public string Data { get; set; }
        public string Message { get; set; }
    }
}
