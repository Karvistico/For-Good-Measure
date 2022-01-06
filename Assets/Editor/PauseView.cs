using Conversa.Runtime;
using Conversa.Runtime.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Conversa.Editor
{
	public class PauseView : BaseNodeView<PauseNode>
	{
		protected override string Title => "Pause Node";

		// Constructors

		public PauseView(Conversation conversation) : base(new PauseNode(), conversation) { }

		public PauseView(PauseNode data, Conversation conversation) : base(data, conversation) { }
	
		[NodeMenuModifier(6)]
		private static void ModifyMenu(NodeMenuTree tree, Conversation conversation)
		{
			tree.AddGroup("My custom nodes");
			tree.AddNode<PauseView>("Pause Node"); 
		}
	}
}