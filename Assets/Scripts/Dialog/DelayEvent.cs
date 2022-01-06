using System;
using Conversa.Runtime.Interfaces;

namespace Conversa.Runtime.Events
{
	public class DelayEvent : IConversationEvent
	{
        public float Value { get; }
		public Action Advance { get; }

		public DelayEvent(float value, Action advance)
		{
            Value = value;
			Advance = advance;	
		}
	}
}
