using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestHarness.MessageBroker
{
    public class MessageBroker
    {
        public delegate void MessageReceivedEventHandler(object sender, MessageBrokerEventArgs e);
        public event MessageReceivedEventHandler MessageReceived;


        
        private static MessageBroker _instance;
        /// <summary>
        /// Use this as a singleton
        /// </summary>
        public static MessageBroker Instance
        {
            get
            {
                if (_instance == null) { _instance = new MessageBroker(); }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }


        public void SendMessage(string msgName, object payLoad)
        {
            MessageBrokerEventArgs arg = new MessageBrokerEventArgs(msgName, payLoad);

            //Raise the message received event
            if (MessageReceived != null)
            {
                MessageReceived(this, arg);
            }
        }

        public void SendMessage(string messageName)
        {
            SendMessage(messageName, null);
        }


    }
}
