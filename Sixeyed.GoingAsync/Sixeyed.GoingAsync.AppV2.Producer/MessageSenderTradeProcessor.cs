using Sixeyed.GoingAsync.Core.Incoming;
using Sixeyed.GoingAsync.Core.Messages;
using Sixeyed.GoingAsync.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NServiceBus;

namespace Sixeyed.GoingAsync.AppV2.Producer
{
    public class MessageSenderTradeProcessor : IIncomingTradeProcessor
    {
        private readonly IBus _bus;
        private readonly ITradeContextFactory _dbFactory;

        public MessageSenderTradeProcessor(ITradeContextFactory dbFactory, IBus bus)
        {
            _dbFactory = dbFactory;
            _bus = bus;
        }

        public void Process(IncomingTrade incomingTrade)
        {
            _bus.Send(new ValidateTradeMessage
            {
                TradeId = incomingTrade.Id
            });

            _bus.Send(new EnrichParty1Message
            {
                TradeId = incomingTrade.Id,
                Party1Lei = incomingTrade.Party1Lei
            });

            _bus.Send(new EnrichParty2Message
            {
                TradeId = incomingTrade.Id,
                Party2Lei = incomingTrade.Party2Lei
            });

            Console.WriteLine("* Sent messages to processed trade with ID: {0}", incomingTrade.Id);
        }

        public void Dispose()
        {

        }
    }
}
