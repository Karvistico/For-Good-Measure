using Conversa.Runtime.Interfaces;

namespace Conversa.Runtime.Events
{
    public class UserEvent : IConversationEvent
    {
        public string Name { get; }

        public UserEvent(string name)
        {
            Name = name;
        }
    }
}