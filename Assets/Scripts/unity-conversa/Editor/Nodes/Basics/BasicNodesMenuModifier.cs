using System.Linq;
using Conversa.Runtime;
using Conversa.Runtime.Nodes;
using Conversa.Runtime.Properties;
using unity_conversa.Editor.Nodes;
using UnityEngine;

namespace Conversa.Editor
{
	public class BasicNodesMenuModifier
	{
		[NodeMenuModifier]
		private static void ModifyMenu(NodeMenuTree tree, Conversation conversation)
		{
			tree.AddGroup("Basics");
			tree.AddNode<MessageNodeView>("Message", 2);
			tree.AddNode<AdvancedMessageNodeView>("Advanced Message", 2);
			tree.AddNode<ChoiceNodeView>("Choice", 2);
			tree.AddNode<ConditionalNodeView>("Branch", 2);
			tree.AddNode<BookmarkNodeView>("Bookmark", 2);
			tree.AddNode<RandomFlowNodeView>("Random", 2);

			tree.AddGroup("Jumps", 2);

			conversation.AllNodes.OfType<BookmarkNode>().ToList().ForEach(bookmark =>
			{
				BookmarkJumpNodeView Callback(Vector2 position)
				{
					var node = new BookmarkJumpNode { BookmarkName = bookmark.Name, BookmarkGuid = bookmark.Guid};
					var view = new BookmarkJumpNodeView(node, conversation);
					view.SetPosition(position);
					return view;
				}

				tree.AddNode("Jump: " + bookmark.Name, Callback, 3);
			});

			tree.AddGroup("Literals");
			tree.AddNode<LiteralStringNodeView>("String", 2);
			tree.AddNode<AbsoluteFloatNodeView>("Float", 2);
			tree.AddNode<AbsoluteBoolNodeView>("Boolean", 2);

			tree.AddGroup("Text tools");
			tree.AddNode<ParseNodeView>("Parse", 2);
			tree.AddNode<FloatToStringNodeView>("ToString", 2);

			tree.AddGroup("Events");

			conversation.Properties.OfType<EventProperty>().ToList().ForEach(property =>
			{
				EventNodeView Callback(Vector2 position)
				{
					var node = new EventNode(property.Guid, property.Name);
					var view = new EventNodeView(node, conversation);
					view.SetPosition(position);
					return view;
				}

				tree.AddNode(property.Name, Callback, 2);
			});
		}
	}
}