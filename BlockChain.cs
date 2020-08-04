using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlockChain
{
    public class BlockChain
    {
        public IList<Block> Chain { set; get; }

        public BlockChain()
        {
            InitializeChain();
            AddGenesisBlock(); 
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, "{}");
        }
        
        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        { 
            return Chain[Chain.Count - 1];
        }
    }
}
