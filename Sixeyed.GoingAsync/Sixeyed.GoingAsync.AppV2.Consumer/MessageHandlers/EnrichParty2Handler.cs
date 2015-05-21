using System;
using System.Diagnostics;
using NServiceBus;
using Sixeyed.GoingAsync.Core.Messages;
using Sixeyed.GoingAsync.Core.Model;
using Sixeyed.GoingAsync.Steps.Enrich;

namespace Sixeyed.GoingAsync.AppV2.Consumer.MessageHandlers
{
    public class EnrichParty2Handler : IHandleMessages<EnrichParty2Message>
    {
        private readonly ITradeContextFactory _dbFactory;

        public EnrichParty2Handler(ITradeContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Handle(EnrichParty2Message message)
        {
            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine("| EnrichPartyHandler | Received trade with message ID: " + message.TradeId);

            using (var db = _dbFactory.GetContext())
            {
                var enricher = new PartyEnricher();
                var trade = db.IncomingTrades.Find(message.TradeId);

                trade.Party2Id = enricher.GetInternalId(trade.Party2Lei);
                trade.ProcessedAt = DateTime.UtcNow;

                db.SaveChanges();
            }

            Console.WriteLine("* | EnrichPartyHandler | Processed trade with ID: {0}, took: {1}ms", message.TradeId, stopwatch.ElapsedMilliseconds);
        }
    }
}