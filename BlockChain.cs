using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FirstBlockChain
{
    public class BlockChain
    {
        public IList<Block> Chain { set; get; }
        public int Difficulty { get; set; } = 2;

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

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Mine(this.Difficulty);
            block.Hash = latestBlock.CalculatedHash();
            Chain.Add(block);
        }

        /*The IsValid method will check two things.
           Each block’s hash to see if the block is changed
           Previous block’s hash to see if the block is changed and recalculated*/
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculatedHash())
                {
                    return false;
                }
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
