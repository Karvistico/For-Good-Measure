using System;
using Conversa.Runtime.Interfaces;

namespace Conversa.Runtime.Events
{
	public class PauseEvent : IConversationEvent
	{
		public Action Advance { get; }

		public PauseEvent(Action advance)
		{
			Advance = advance;	
		}
	}
}
