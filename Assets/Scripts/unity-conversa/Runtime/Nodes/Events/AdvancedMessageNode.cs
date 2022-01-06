using System;
using System.Linq;
using Conversa.Runtime.Events;
using Conversa.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Conversa.Runtime.Nodes
{
    [MovedFrom(true, null, "Assembly-CSharp")]
    [Serializable]
    [Port("Previous", "previous", typeof(BaseNode), Flow.In, Capacity.Many)]
    [Port("Next", "next", typeof(BaseNode), Flow.Out, Capacity.One)]
    public class AdvancedMessageNode : BaseNode, IEventNode
    {
        [SerializeField]
        private string actor;
        
        [SerializeField]
        private string message;
        
        [Slot("Actor", "actor", Flow.In, Capacity.One)]
        public string Actor
        {
            get => actor;
            set => actor = value;
        }

        [Slot("Message", "message", Flow.In, Capacity.One)]
        public string Message
        {
            get => message;
            set => message = value;
        }
        
        public void Process(Conversation conversation, ConversationEvents conversationEvents)
        {
            var finalActor = conversation.IsConnected(Guid, "actor")
                ? conversation.GetConnectedValueTo<string>(this, "actor")
                : actor;
            var finalMessage = conversation.IsConnected(Guid, "message")
                ? conversation.GetConnectedValueTo<string>(this, "message")
                : message;

            var e = new MessageEvent(finalActor, finalMessage, () =>
            {
                var nextNode = conversation.GetOppositeNodes(GetNodePort("next")).FirstOrDefault();
                conversation.Process(nextNode, conversationEvents);
            });

            conversationEvents.OnMessage.Invoke(e); // Deprecated
            conversationEvents.OnConversationEvent.Invoke(e);
        }
    }
}