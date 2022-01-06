using Conversa.Runtime;
using Conversa.Runtime.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Conversa.Editor
{
	public class ConditionalNodeView : BaseNodeView<ConditionalNode>
	{
		protected override string Title => "Branch";

		// Constructors

		public ConditionalNodeView(Conversation conversation) : base(new ConditionalNode(), conversation) { }

		public ConditionalNodeView(ConditionalNode data, Conversation conversation) : base(data, conversation) { }

		// Methods

		protected override void SetBody() { }
	}
}