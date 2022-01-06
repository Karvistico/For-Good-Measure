using Conversa.Runtime;

namespace Conversa.Editor.Nodes.LogicalOperators
{
	public class LogicalNodesMenuModifier
	{
		[NodeMenuModifier(3)]
		private static void ModifyMenu(NodeMenuTree tree, Conversation conversation)
		{
			tree.AddGroup("Logical operators");
			tree.AddNode<AndNodeView>("And", 2);
			tree.AddNode<OrNodeView>("Or", 2);
			tree.AddNode<NorNodeView>("Nor", 2);
			tree.AddNode<NorNodeView>("Xor", 2);
			tree.AddNode<NotNodeView>("Not", 2);
		}
	}
}