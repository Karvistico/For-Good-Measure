using System;
using System.Collections.Generic;
using Conversa.Editor.Interfaces;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Conversa.Editor
{
	public class NodeMenuTree
	{
		public List<SearchTreeEntry> tree;
		private ConversaGraphView graphView;

		public NodeMenuTree(ConversaGraphView graphView)
		{
			tree = new List<SearchTreeEntry> { CreateGroupEntry("Add node", 0) };
			this.graphView = graphView;
		}

		private static SearchTreeEntry CreateGroupEntry(string name, int level = 1) =>
			new SearchTreeGroupEntry(new GUIContent(name), level);

		private static GUIContent CreateContent(string name) => new GUIContent(name, Texture2D.redTexture);

		private static SearchTreeEntry CreateEntry(string name, Action<Vector2> callback, int level = 1) =>
			new SearchTreeEntry(CreateContent(name)) { userData = callback, level = level };

		public void AddNode<TView>(string name, int level = 1)
		{
			void Callback(Vector2 position) => graphView.AddNode(position, typeof(TView));
			tree.Add(CreateEntry(name, Callback, level));
		}

		public void AddNode<TView>(string name, Func<Vector2, TView> createNode, int level) where TView : INodeView
		{
			void Callback(Vector2 position)
			{
				var nodeView = createNode(position);
				graphView.AddNewNode(nodeView);
			}

			tree.Add(CreateEntry(name, Callback, level));

		}

		public void AddGroup(string groupName, int level = 1) =>
			tree.Add(CreateGroupEntry(groupName, level));
	}
}