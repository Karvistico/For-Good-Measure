namespace Conversa.Runtime.Interfaces
{
	public interface IEventNode
	{
		void Process(Conversation conversation, ConversationEvents conversationEvents);
	}
}