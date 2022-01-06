using Conversa.Runtime;
using Conversa.Runtime.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Conversa.Editor
{
    public class MessageNodeInspector : BaseNodeInspector<MessageNode>
    {
        public MessageNodeInspector(MessageNode data, Conversation conversation) : base(data, conversation) { }

        protected override void SetBody()
        {
            var template = Resources.Load<VisualTreeAsset>("Inspectors/MessageNode");
            template.CloneTree(this);
            
            var inputActor = this.Q<TextField>("actor");
            inputActor.RegisterValueChangedCallback(UpdateActor);
            inputActor.SetValueWithoutNotify(data.actor);
            inputActor.isDelayed = true;

            var inputMessage = this.Q<TextField>("body");
            inputMessage.RegisterValueChangedCallback(UpdateMessage);
            inputMessage.SetValueWithoutNotify(data.message);
            inputMessage.isDelayed = true;
        }
        
        private void UpdateActor(ChangeEvent<string> evt)
        {
            RegisterUndoStep();
            data.actor = evt.newValue;
        }

        private void UpdateMessage(ChangeEvent<string> evt)
        {
            RegisterUndoStep();
            data.message = evt.newValue;
        }
    }
}