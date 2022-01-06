using Conversa.Editor.Nodes.Math;
using Conversa.Runtime;

namespace Conversa.Editor
{
	public class MathNodesMenuModifier
	{
		[NodeMenuModifier(3)]
		private static void ModifyMenu(NodeMenuTree tree, Conversation conversation)
		{
			tree.AddGroup("Math operators");

			tree.AddNode<AddNodeView>("Add", 2);
			tree.AddNode<SubtractNodeView>("Subtract", 2);
			tree.AddNode<MultiplyNodeView>("Multiply", 2);
			tree.AddNode<DivideNodeView>("Divide", 2);

			tree.AddNode<CompareNodeView>("Compare number", 2);
			tree.AddNode<RandomFloatView>("Random", 2);
			tree.AddGroup("Obsolete", 2);
			tree.AddNode<GreaterThanNodeView>("Greater than", 3);
			tree.AddNode<LessThanNodeView>("Less than", 3);
			tree.AddNode<EqualsNodeView>("Equals", 3);
		}
	}
}

