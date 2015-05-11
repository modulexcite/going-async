using System;
using System.Diagnostics;
using NServiceBus;
using Sixeyed.GoingAsync.Core.Messages;
using Sixeyed.GoingAsync.Core.Model;
using Sixeyed.GoingAsync.Steps.Enrich;

namespace Sixeyed.GoingAsync.AppV2.Consumer.MessageHandlers
{
	public class EnrichParty1Handler : IHandleMessages<EnrichParty1Message>
	{
		private readonly ITradeContextFactory _dbFactory;

		public EnrichParty1Handler( ITradeContextFactory dbFactory )
		{
			_dbFactory = dbFactory;
		}

		public void Handle( EnrichParty1Message message )
		{
			var stopwatch = Stopwatch.StartNew();

			Console.WriteLine( "| EnrichPartyHandler | Received trade with message ID: " + message.TradeId );

			using( var db = _dbFactory.GetContext() )
			{
				var enricher = new PartyEnricher();
				var trade = db.IncomingTrades.Find( message.TradeId );

				trade.Party1Id = enricher.GetInternalId( trade.Party1Lei );
				trade.ProcessedAt = DateTime.UtcNow;
				
				db.SaveChanges();
			}

			Console.WriteLine( "* | EnrichPartyHandler | Processed trade with ID: {0}, took: {1}ms", message.TradeId, stopwatch.ElapsedMilliseconds );
		}
	}
}