using System;
using System.Collections.Generic;
using System.Text;
using Azure.Messaging.ServiceBus;


namespace Listener.bus
{
    class Busclient
    {
        private static readonly string CONN_STRING = "Endpoint=sb://serviceenvf.servicebus.windows.net/;SharedAccessKeyName=Listener;SharedAccessKey=vn2h4wJPjTadnq4GLqhj34CEof4nSEJ0KLbC8V739Tg=";
        

        public static ServiceBusClient Cliente { get; private set; }

        static Busclient() { InitBus(); }

        private static void InitBus() {
            if (Cliente==null) {
                Cliente = new ServiceBusClient(CONN_STRING);
            }
        }


    }
}
