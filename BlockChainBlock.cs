using Microsoft.DotNet.Cli.Utils.CommandParsing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlockChain
{
    public class BlockChainBlock
    {
        BlockChain ghostCoin = new BlockChain();
        public void NewBlock()
        {
            ghostCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Joe,receiver:MaHesh,amount:10}"));
            ghostCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Joe,amount:5}"));
            ghostCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Joe,amount:5}"));
            Console.WriteLine(JsonConvert.SerializeObject(ghostCoin, Formatting.Indented));
        }


        /*One of the advantages of using blockchain is data security.Data security means that tampering with the old data and altering the method 
          of securing new data is prevented by both the cryptographic method and the non-centralized storage of the data itself.
          However, blockchain is just a data structure in which data can be easily changed like this.*/
        public void ChangeData()
        {
            ghostCoin.Chain[1].Data = "{sender:Joe,receiver:MaHesh,amount:1000}";
        }
       
        public bool IsValid()
        {
            for (int i = 1; i < ghostCoin.Chain.Count; i++)
            {

            }
            return true;
        }

    }

}
