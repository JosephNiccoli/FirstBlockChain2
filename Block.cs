using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlockChain
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public  string PreviousHash { get; set; }
        public string Hash { get; set; }

        // replace data with transaction
        //public string Data { get; set; }

        /*After added Transaction class, we can update our Block to replace Data with Transactions. 
         * One misunderstanding I had before was that one Blockchain can only contain one transaction. 
         * Actually, one block can contain many transactions. Therefore, we uses a collection to store transaction data. */
        public IList<Transaction> Transactions { get; set; }

        // added nonce to the property
        public int Nonce { get; set; } = 0;

        public Block (DateTime timeStamp, string previousHash, IList<Transaction> transactions)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            //  Data = data;
            Transactions = transactions;
            Hash = CalculatedHash();

        }
        //CalculateHash method is updated to include nonce in hash generating. - nonce - proof of work
        //The CalculateHash method is also updated to use Transactions instead of Data to get hash of a block. - transactions
        public string CalculatedHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}");

            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }


        /*In the end, a new method, Mine, is added to accept difficulty as a parameter. 
         * The difficulty is an integer that indicates the number of leading zeros required for a generated hash. 
         * The Mine method tries to find a hash that matches with difficulty. 
         * If a generated hash doesn’t meet the difficulty, then it increases nonce to generate a new one.
         * The process will be ended when a qualified hash is found.*/
        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculatedHash();
            }
        }
    }
}
