using Conversa.Runtime;
using Conversa.Runtime.Nodes;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Conversa.Editor
{
	public class DelayView : BaseNodeView<DelayNode>
	{
        FloatField delayField;

		protected override string Title => "Delay Node";

		// Constructors

		public DelayView(Conversation conversation) : base(new DelayNode(), conversation) { }

		public DelayView(DelayNode data, Conversation conversation) : base(data, conversation) { }

        // Methods
        
        protected override void SetBody()
        {
            delayField = new FloatField();
            delayField.SetValueWithoutNotify(Data.delay);
            delayField.RegisterValueChangedCallback(e => Data.delay = e.newValue);
            delayField.isDelayed = true;

            var wrapper = new VisualElement();
            wrapper.AddToClassList("p-5");
            wrapper.Add(delayField);

            bodyContainer.Add(wrapper);
        }
	
		[NodeMenuModifier(6)]
		private static void ModifyMenu(NodeMenuTree tree, Conversation conversation)
		{
			tree.AddGroup("My custom nodes");
			tree.AddNode<DelayView>("Delay Node"); 
		}
	}
}