using Apache.NMS;
using Apache.NMS.Util;
using System;

namespace ActiveMQSubscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri connectionUri = new Uri("tcp://localhost:61616?wireFormat.cacheEnabled=false&wireFormat.tightEncodingEnabled=false");
            IConnectionFactory connectionFactory = new NMSConnectionFactory(connectionUri);
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (ISession session = connection.CreateSession())
                {
                    IDestination destination = SessionUtil.GetDestination(session, "queue://test");
                    using (IMessageConsumer consumer = session.CreateConsumer(destination))
                    {
                        connection.Start();
                        //producer.DeliveryMode = MsgDeliveryMode.Persistent;
                        //ITextMessage request = session.CreateTextMessage("mensaje de Prueba");
                        //request.NMSCorrelationID = "abc";
                        //request.Properties["NMSXGroupID"] = "cheese";
                        //request.Properties["myHeader"] = "Cheddar";
                        //producer.Send(request);
                        ITextMessage message=consumer.Receive() as ITextMessage;
                        if (message == null)
                        {
                            Console.WriteLine("No message received!");
                        }
                        else
                        {
                            Console.WriteLine("Received message with ID:   " + message.NMSMessageId);
                            Console.WriteLine("Received message with text: " + message.Text);
                        }
                    }
                }
            }
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
