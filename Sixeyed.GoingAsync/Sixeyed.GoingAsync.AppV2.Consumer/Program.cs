using System;
using Args;
using Microsoft.Practices.Unity;
using NServiceBus;
using Sixeyed.GoingAsync.Core.Model;

namespace Sixeyed.GoingAsync.AppV2.Consumer
{
    class Program
    {
        //private static MessageHandlerFactory _HandlerFactory;

        static void Main(string[] args)
        {
            var arguments = Configuration.Configure<Arguments>().CreateAndBind(args);

            var container = new UnityContainer();
            container.RegisterType<ITradeContextFactory, TradeContextFactory>();

            //container.RegisterType<IMessageHandler, ValidateTradeHandler>("ValidateTradeHandler");
            //container.RegisterType<IMessageHandler, EnrichPartyHandler>("EnrichParty1Handler",
            //											new InjectionConstructor(
            //													new ResolvedParameter<ITradeContextFactory>(),
            //													true, false));
            //container.RegisterType<IMessageHandler, EnrichPartyHandler>("EnrichParty2Handler",
            //											new InjectionConstructor(
            //													new ResolvedParameter<ITradeContextFactory>(),
            //													false, true));


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








            //container.RegisterType<MessageHandlerFactory>();

            //_HandlerFactory = container.Resolve<MessageHandlerFactory>();

            //var arguments = Args.Configuration.Configure<Arguments>().CreateAndBind(args);
            //var queue = new MessageQueue(arguments.InputQueue, QueueAccessMode.Receive);
            //while (true)
            //{
            //	var msg = queue.Receive();
            //	Handle(msg);
            //}
        }

        //private static void Handle(Message msg)
        //{
        //	var messageType = msg.Label;
        //	var handlers = _HandlerFactory.GetHandlers(messageType);
        //	if (handlers.Any())
        //	{
        //		string json;
        //		using (var reader = new StreamReader(msg.BodyStream))
        //		{
        //			json = reader.ReadToEnd();
        //		}
        //		var message = JsonConvert.DeserializeObject<ValidateTradeMessage>(json) as IMessage;
        //		foreach (var handler in handlers)
        //		{
        //			handler.Handle(message);
        //		}
        //	}
        //}
    }
}
