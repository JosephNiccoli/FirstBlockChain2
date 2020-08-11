using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FirstBlockChain
{
    public class Program
    {
        static void Main(string[] args)
        {
            BlockChain ghostCoin = new BlockChain();

            ghostCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Joe,receiver:MaHesh,amount:10}"));
            ghostCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Joe,amount:5}"));
            ghostCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Joe,amount:5}"));
            Console.WriteLine(JsonConvert.SerializeObject(ghostCoin, Formatting.Indented));


            /*One of the advantages of using blockchain is data security.Data security means that tampering with the old data and altering the method 
              of securing new data is prevented by both the cryptographic method and the non-centralized storage of the data itself.
              However, blockchain is just a data structure in which data can be easily changed like this.*/

            ghostCoin.Chain[1].Data = "{sender:Joe,receiver:MaHesh,amount:1000}";


            Console.WriteLine($"Is Chain Valid: {ghostCoin.IsValid()}");

            Console.WriteLine($"Update amount to 1000");
            ghostCoin.Chain[1].Data = "{sender:Joe,receiver:MaHesh,amount:1000}";

            Console.WriteLine($"Is Chain Valid: {ghostCoin.IsValid()}");

            //How about the case when the attacker recalculates the hash of the tampered block?
            //The Validation result will still be false because the validation not only looks at the current block but also at the link to the previous block.
            ghostCoin.Chain[1].Hash = ghostCoin.Chain[1].CalculatedHash();

            //Now, what about the case when an attacker recalculates hashes of all the current block and the following blocks?
            //After all the Blocks are recalculated, the verification is passed. However, this is only passed on one node because Blockchain is a decentralized system. Tampering with one node could be easy but tampering with all the nodes in the system is impossible.

            Console.WriteLine($"Update the entire chain");
            ghostCoin.Chain[2].PreviousHash = ghostCoin.Chain[1].Hash;
            ghostCoin.Chain[2].Hash = ghostCoin.Chain[2].CalculatedHash();
            ghostCoin.Chain[3].PreviousHash = ghostCoin.Chain[2].Hash;
            ghostCoin.Chain[3].Hash = ghostCoin.Chain[3].CalculatedHash();

        }
    }
}
