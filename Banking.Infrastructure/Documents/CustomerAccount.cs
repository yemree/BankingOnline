using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Infrastructure.Documents
{
    public class CustomerAccount : Document
    {
        public string CustomerId { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
