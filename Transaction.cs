using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlockChain
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int Amount { get; set; }

        // constructor
        public Transaction(String fromAddress, string toAddress, int amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
        }
    }
}
  