using System;
using System.Diagnostics;
using System.Linq;
using NServiceBus;
using Sixeyed.GoingAsync.Core.Messages;
using Sixeyed.GoingAsync.Core.Model;
using Sixeyed.GoingAsync.Steps.Validate;

namespace Sixeyed.GoingAsync.AppV2.Consumer.MessageHandlers
{
	public class ValidateTradeHandler : IHandleMessages<ValidateTradeMessage>
    {
        protected ITradeContextFactory _dbFactory;

        public ValidateTradeHandler(ITradeContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
		public void Handle( ValidateTradeMessage message )
		{
			var stopwatch = Stopwatch.StartNew();

			Console.WriteLine( "| ValidateTradeHandler | Received trade with message ID: " + message.TradeId );

			using( var db = _dbFactory.GetContext() )
			{
				var trade = db.IncomingTrades.Find( message.TradeId );

				var validator = new FpmlValidator();
				var failures = validator.Validate( trade.Fpml );
				trade.IsFpmlValid = failures.Any() == false;

				trade.ProcessedAt = DateTime.UtcNow;
				db.SaveChanges();
			}

			Console.WriteLine( "* | ValidateTradeHandler | Processed trade with ID: {0}, took: {1}ms", message.TradeId, stopwatch.ElapsedMilliseconds );
		}
	}
}