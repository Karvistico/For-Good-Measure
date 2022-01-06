using System.Collections.Generic;
using System.Linq;
using Conversa.Editor.Utils;
using Conversa.Runtime.Events;
using Conversa.Runtime.Interfaces;
using Conversa.Runtime.Nodes;
using Conversa.Runtime.Properties;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting.APIUpdating;

namespace Conversa.Runtime
{
	[MovedFrom(true, null, "Assembly-CSharp")]
	[CreateAssetMenu(fileName = "New conversation", menuName = "Conversa/Conversation", order = 1)]
	public class Conversation : ScriptableObject
	{
		[SerializeField]
		private BookmarkNode startNode = new BookmarkNode
		{ Name = "Start", NodeRect = new Rect(100, 100, 100, 100) };

		public BookmarkNode StartNode => startNode;

		[SerializeReference] private List<INode> nodes;
		[SerializeReference] private List<EdgeData> edges;
		[SerializeReference] private List<BaseProperty> properties = new List<BaseProperty>();
		[SerializeReference] private List<StickyNoteData> stickyNotes = new List<StickyNoteData>();
		[SerializeReference] private List<GroupData> groups = new List<GroupData>();

		public IEnumerable<INode> AllNodes => nodes.Prepend(startNode);
		public IEnumerable<INode> UserNodes => nodes;
		public List<EdgeData> Edges => edges;
		public List<GroupData> Groups => groups;
		public List<StickyNoteData> StickyNotes => stickyNotes;
		public List<BaseProperty> Properties => properties;
		public UnityEvent OnPropertyListModified { get; } = new UnityEvent();

		// Helper

		public Conversation Clone() => Instantiate(this);

		public void Apply(Conversation other)
		{
			nodes = other.nodes;
			edges = other.edges;
			startNode = other.startNode;
			properties = other.properties;
			stickyNotes = other.StickyNotes;
			groups = other.Groups;
		}

		// Validation

		public void Validate()
		{
			Sanitizer.UpdateJumpNodes(this);

			var nodesRemovedCount = nodes.RemoveAll(node => !IsValidNode(node));
			var edgesRemovedCount = edges.RemoveAll(edge => !IsValidEdge(edge));

			if (nodesRemovedCount > 0)
				Debug.LogWarning($"{nodesRemovedCount} invalid nodes were removed.");

			if (edgesRemovedCount > 0)
				Debug.LogWarning($"{edgesRemovedCount} invalid edges were removed.");

			nodes = nodes.Select(Sanitizer.ReplaceBooleanNodes).ToList();
		}

		// Checks

		private bool PortExists(NodePort nodePort, Flow flow)
		{
			var node = GetNode(nodePort.Node);
			return node != null && node.ContainsPort(nodePort.Port, flow);
		}

		private bool IsValidNode(INode node) => node != null && node.IsValid(this);

		private bool IsValidEdge(EdgeData edgeData)
		{
			if (edgeData == null)
			{
				Debug.LogWarning("edgeData is null");
				return false;
			}

			if (!edgeData.IsValid())
			{
				Debug.LogWarning("edgeData is not valid");
				return false;
			}

			if (!PortExists(edgeData.Output, Flow.Out))
			{
				Debug.LogWarning("Output nodePort does not exist");
				return false;
			}

			if (!PortExists(edgeData.Input, Flow.In))
			{
				Debug.LogWarning("Input nodePort does not exist");
				return false;
			}

			return true;
		}

		public bool IsGrouped(string id) => groups.Any(group => group.Elements.Contains(id));

		// Add

		public void AddEdge(EdgeData edgeData) => edges.Add(edgeData);

		public void AddNode(INode baseNode)
		{
			var node = GetNode(baseNode.Guid);
			if (node == null) nodes.Add(baseNode);
		}

		// Remove

		public void RemoveNode(string guid) => nodes.RemoveAll(x => x.Guid == guid);

		public void RemoveEdge(EdgeData edgeData) => edges.RemoveAll(x => x.Equals(edgeData));

		public void RemoveNote(string guid) => StickyNotes.RemoveAll(x => x.Id == guid);


		// Get

		public GroupData GetGroup(string id) => Groups.FirstOrDefault(x => x.Id == id);

		public StickyNoteData GetNote(string guid) => StickyNotes.FirstOrDefault(note => note.Id == guid);

		public BookmarkNode GetBookmark(string guid) =>
			AllNodes.OfType<BookmarkNode>().FirstOrDefault(x => x.Guid == guid);

		private INode GetNode(string nodeGuid) => AllNodes.FirstOrDefault(x => x.Guid == nodeGuid);

		private IEnumerable<EdgeData> GetEdgesConnected(NodePort nodePort) =>
			edges.Where(edge => edge.Contains(nodePort));

		private IEnumerable<NodePort> GetOppositeNodePorts(NodePort nodePort) =>
			GetEdgesConnected(nodePort).Select(x => x.Opposite(nodePort));

		public IEnumerable<INode> GetOppositeNodes(NodePort nodePort) =>
			 GetOppositeNodePorts(nodePort).Select(x => GetNode(x.Node));

		public bool IsConnected(string nodeGuid, string portGuid)
		{
			var nodePort = new NodePort(nodeGuid, portGuid);
			return edges.Any(edge => edge.Contains(nodePort));
		} 
		
		private T GetConnectedValueTo<T>(string nodeGuid, string portGuid)
		{
			var nodePort = new NodePort(nodeGuid, portGuid);

			var oppositeNodePort = GetOppositeNodePorts(nodePort).FirstOrDefault();

			if (oppositeNodePort == null) return default;

			var node = GetNode(oppositeNodePort.Node);

			if (node is IValueNode valueNode)
				return valueNode.GetValue<T>(oppositeNodePort.Port, this);

			return default;
		}

		public T GetConnectedValueTo<T>(BaseNode node, string portGuid) =>
			GetConnectedValueTo<T>(node.Guid, portGuid);

		public void Process(INode node, ConversationEvents events)
		{
			if (node is IEventNode eventNode)
				eventNode.Process(this, events);
			else
			{
				events.OnEnd.Invoke(); // Deprecated
				events.OnConversationEvent.Invoke(new EndEvent());
			}
		}

		#region Properties

		//
		// Properties
		//

		// TODO for v2: The whole property logic could be moved into a "PropertySet"

		public void AddProperty<T>(T property) where T : BaseProperty
		{
			properties.Add(property);
			OnPropertyListModified.Invoke();
		}

		public void RemoveProperty(string guid)
		{
			properties.RemoveAll(x => x.Guid == guid);
			OnPropertyListModified.Invoke();
		}

		public void EditProperty(string guid, string newName)
		{
			GetProperty(guid).Name = newName;
			OnPropertyListModified.Invoke();
		}

		public BaseProperty GetProperty(string guid) => properties.FirstOrDefault(x => x.Guid == guid);

		public IEnumerable<IValueProperty> GetValueProperties() =>
			Properties.Where(x => x is IValueProperty).Cast<IValueProperty>();

		public bool PropertyExists(string guid) => properties.Any(property => property.Guid == guid);

		public T GetProperty<T>(string propertyName)
		{
			var property = properties.FirstOrDefault(x => x.Name == propertyName);

			switch (property)
			{
				case null:
					Debug.LogWarning($"Tried to get a non-existent property: '{propertyName}'");
					return default;

				case ValueProperty<T> valueProperty:
					return valueProperty.Value;

				default:
					Debug.LogWarning($"Tried to get a property with wrong type: '{propertyName}'");
					return default;
			}
		}

		public IValueProperty GetPropertyByGuid(string guid)
		{
			var property = properties.OfType<IValueProperty>().FirstOrDefault(x => x.Guid == guid);

			if (property != null) return property;
			
			Debug.LogWarning($"Tried to get a non-existent property: '{guid}'");
			return default;
		}

		// Used by the user to update values
		public void SetProperty<T>(string propertyName, T value)
		{
			var property = properties.FirstOrDefault(x => x.Name == propertyName);

			if (property is ValueProperty<T> valueProperty)
				valueProperty.Value = value;
		}

		#endregion
	}
}
