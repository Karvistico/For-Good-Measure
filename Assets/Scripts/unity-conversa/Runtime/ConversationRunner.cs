using System;
using System.Linq;
using Conversa.Runtime.Events;
using Conversa.Runtime.Interfaces;
using UnityEngine.Events;

namespace Conversa.Runtime
{
	public class ConversationRunner
	{
		private readonly Conversation conversation;

		private readonly ConversationEvents conversationEvents = new ConversationEvents();

		#region deprecated

		public UnityEvent<MessageEvent> OnMessage => conversationEvents.OnMessage;
		public UnityEvent<ChoiceEvent> OnChoice => conversationEvents.OnChoice;
		public UnityEvent<UserEvent> OnUserEvent => conversationEvents.OnUserEvent;
		public UnityEvent OnEnd => conversationEvents.OnEnd;

		#endregion

		public UnityEvent<IConversationEvent> OnConversationEvent => conversationEvents.OnConversationEvent;

		public ConversationRunner(Conversation conversation) => this.conversation = conversation;

		private Conversation clone;

		public void Begin()
		{
			clone = conversation.Clone();
			clone.GetValueProperties().ToList().ForEach(x => x.Reset());
			clone.StartNode.Process(clone, conversationEvents);
		}

		public void SetProperty<T>(string name, T value) => clone.SetProperty(name, value);
	}
}