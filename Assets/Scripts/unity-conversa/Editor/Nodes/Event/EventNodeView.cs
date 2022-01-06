using System.Linq;
using Conversa.Editor.Interfaces;
using Conversa.Runtime;
using Conversa.Runtime.Nodes;
using Conversa.Runtime.Properties;

namespace Conversa.Editor
{
	public class EventNodeView : BaseNodeView<EventNode>
	{
		public string PropertyGuid => Data.EventGuid;

		protected override string Title => $"Event: {Data.EventName}";

		public EventNodeView(EventNode node, Conversation conversation) : base(node, conversation) { }

		protected override void SetBody() { }

		public void UpdateName(string newName)
		{
			title = $"Event: {newName}"; ;
		}

		[DeletePropertyHandler]
		public static void HandleDeleteProperty(ConversaGraphView graphView, string propertyGuid)
		{
			var nodesToRemove = graphView
				.GetNodes<EventNodeView>()
				.Where(view => view.PropertyGuid == propertyGuid)
				.Cast<INodeView>()
				.ToList();
			
			graphView.DeleteNodes(nodesToRemove);
		}
		
		[EditPropertyHandler]
		public static void HandleEditEvent(ConversaGraphView graphView, BaseProperty property)
		{
			graphView
				.GetNodes<EventNodeView>()
				.Where(view => view.PropertyGuid == property.Guid)
				.ToList()
				.ForEach(view => view.UpdateName(property.Name));
		}
	}
}