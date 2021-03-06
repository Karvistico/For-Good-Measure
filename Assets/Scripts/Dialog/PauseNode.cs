using System;
using System.Linq;
using Conversa.Runtime.Events;
using Conversa.Runtime.Interfaces;

namespace Conversa.Runtime.Nodes
{
	[Serializable]
	[Port("Previous", "previous", typeof(BaseNode), Flow.In, Capacity.Many)]
	[Port("Next", "next", typeof(BaseNode), Flow.Out, Capacity.One)]
	public class PauseNode : BaseNode, IEventNode
	{
		public void Process(Conversation conversation, ConversationEvents conversationEvents)
		{
			var e = new PauseEvent(() =>
			{
				var nextNode = conversation.GetOppositeNodes(GetNodePort("next")).FirstOrDefault();
				conversation.Process(nextNode, conversationEvents);
			}
			);

			conversationEvents.OnConversationEvent.Invoke(e);
		}
	}
}