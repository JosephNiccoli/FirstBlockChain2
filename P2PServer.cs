using Microsoft.AspNetCore.Server.IIS.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace FirstBlockChain.P2P
{
    public class P2PServer : WebSocketBehavior
    {
        bool chainSynched = false;
        WebSocketServer wss = null;

        public void Start()
        {
            wss = new WebSocketServer($"ws://127.0.0.1:{Program.Port}");
            wss.AddWebSocketService<P2PServer>("/BlockChain");
            wss.Start();
            Console.WriteLine($"Started server at ws://127.0.0.1:{Program.Port}");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                Send("hi Client");
            }
            else
            {
                BlockChain newChain = JsonConvert.DeserializeObject<BlockChain>(e.Data);
                if (newChain.IsValid() && newChain.Chain.Count > Program.GhostCoin.Chain.Count)
                {
                    List<Transaction> newTransactions = new List<Transaction>();
                    newTransactions.AddRange(newChain.PendingTransactions);
                    newTransactions.AddRange(Program.GhostCoin.PendingTransactions);

                    newChain.PendingTransactions = newTransactions;
                    Program.GhostCoin = newChain;
                }

                if (!chainSynched)
                {
                    Send(JsonConvert.SerializeObject(Program.GhostCoin));
                    chainSynched = true;
                }
            }
        }
    }
}
