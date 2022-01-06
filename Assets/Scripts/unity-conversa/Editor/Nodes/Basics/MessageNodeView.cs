using Conversa.Runtime;
using Conversa.Runtime.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Conversa.Editor
{
	public class MessageNodeView : BaseNodeView<MessageNode>
	{
		protected override string Title => "Message";

		// Constructors

		public MessageNodeView(Conversation conversation) : base(new MessageNode(), conversation) { }

		public MessageNodeView(MessageNode data, Conversation conversation) : base(data, conversation) { }

		// Methods

		protected override void SetBody()
		{
			var template = Resources.Load<VisualTreeAsset>("NodeViews/MessageNode");
			template.CloneTree(bodyContainer);
			schedule.Execute(UpdateValues).Every(100);
		}

		private void UpdateValues()
		{
			bodyContainer.Q<Label>("actor").text= Data.actor;
			bodyContainer.Q<Label>("message").text= Data.message;
		}
	}
}