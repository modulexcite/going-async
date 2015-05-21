using Microsoft.Practices.Unity;
using Sixeyed.GoingAsync.Core;
using Sixeyed.GoingAsync.Core.Incoming;
using Sixeyed.GoingAsync.Core.Model;
using System;
using NServiceBus;
using NServiceBus.Config;

namespace Sixeyed.GoingAsync.AppV2.Producer
{
	class Program
	{
		static void Main( string[] args )
		{
			var container = new UnityContainer();
			container.RegisterType<ITradeContextFactory, TradeContextFactory>();
			container.RegisterType<IIncomingTradeProcessor, MessageSenderTradeProcessor>();

			var path = Config.Get( "FileWatcher.Path" );
			container.RegisterType<IncomingTradeFileWatcher>( new InjectionConstructor(
																new ResolvedParameter<IIncomingTradeProcessor>(),
																new ResolvedParameter<ITradeContextFactory>(),
																path ) );

			var cfg = new BusConfiguration();
			cfg.Conventions().DefiningMessagesAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Messages" ) );
			cfg.UsePersistence<InMemoryPersistence>();
			cfg.UseContainer<UnityBuilder>( cusomize =>
			{
				cusomize.UseExistingContainer( container );
			} );

			using( Bus.Create( cfg ).Start() )
			using (var watcher = container.Resolve<IncomingTradeFileWatcher>())
			{
				watcher.Start( true );
				Console.WriteLine( " ** v2.0 NServiceBus IncomingTradeFileWatcher listening (Enter to exit) **" );
				Console.ReadLine();
			}
		}
	}
}
