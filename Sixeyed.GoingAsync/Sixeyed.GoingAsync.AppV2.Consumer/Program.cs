using System;
using Args;
using Microsoft.Practices.Unity;
using NServiceBus;
using Sixeyed.GoingAsync.Core.Model;

namespace Sixeyed.GoingAsync.AppV2.Consumer
{
	class Program
	{
		static void Main(string[] args)
		{
			var arguments = Configuration.Configure<Arguments>().CreateAndBind(args);

			var container = new UnityContainer();
			container.RegisterType<ITradeContextFactory, TradeContextFactory>();

			var cfg = new BusConfiguration();
			cfg.EndpointName(arguments.EndpointName);
			cfg.Conventions().DefiningMessagesAs(t => t.Namespace != null && t.Namespace.EndsWith(".Messages"));
			cfg.UsePersistence<InMemoryPersistence>();
			cfg.UseContainer<UnityBuilder>(customize =>
			{
				customize.UseExistingContainer(container);
			});

			using (Bus.Create(cfg).Start())
			{
				Console.WriteLine("Endpoint " + arguments.EndpointName + " started...");
				Console.ReadLine();
			}
		}
	}
}
