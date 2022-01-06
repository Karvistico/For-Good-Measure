using System;
using System.Linq;
using Conversa.Runtime.Events;
using Conversa.Runtime.Interfaces;
using UnityEngine.Scripting.APIUpdating;

namespace Conversa.Runtime.Nodes
{
	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Previous", "previous", typeof(BaseNode), Flow.In, Capacity.Many)]
	[Port("Next", "next", typeof(BaseNode), Flow.Out, Capacity.One)]
	public class MessageNode : BaseNode, IEventNode
	{
		public string actor;
		public string message;

		public void Process(Conversation conversation, ConversationEvents conversationEvents)
		{
			var e = new MessageEvent(actor, message, () =>
			{
				var nextNode = conversation.GetOppositeNodes(GetNodePort("next")).FirstOrDefault();
				conversation.Process(nextNode, conversationEvents);
			});

			conversationEvents.OnMessage.Invoke(e); // Deprecated
			conversationEvents.OnConversationEvent.Invoke(e);
		}
	}
}