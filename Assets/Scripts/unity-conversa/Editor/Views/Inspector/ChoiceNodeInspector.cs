using Conversa.Runtime;
using Conversa.Runtime.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Conversa.Editor
{
    public class ChoiceNodeInspector : BaseNodeInspector<ChoiceNode>
    {
        public ChoiceNodeInspector(ChoiceNode data, Conversation conversation) : base(data, conversation) { }

        protected override void SetBody()
        {
            var template = Resources.Load<VisualTreeAsset>("Inspectors/ChoiceNode");
            template.CloneTree(this);

            var actorInput = this.Q<TextField>("actor");
            actorInput.RegisterValueChangedCallback(HandleUpdateActor);
            actorInput.SetValueWithoutNotify(data.Actor);
            actorInput.isDelayed = true;

            var messageInput = this.Q<TextField>("message");
            messageInput.RegisterValueChangedCallback(HandleUpdateMessage);
            messageInput.SetValueWithoutNotify(data.Message);
            messageInput.isDelayed = true;

            data.Options.ForEach(SetOption);

            this.Q<Button>(classes: "add-option").clickable.clicked += HandleAddOption;
        }

        private void SetOption(PortDefinition<BaseNode> portDefinition)
        {
            var optionsWrapper = this.Q(classes: "option-list");
            
            var option = new ChoiceOptionForm(portDefinition);
            option.OnDelete.AddListener(() => HandleDeleteOption(option));
            optionsWrapper.Add(option);
        }

        private void HandleAddOption()
        {
            var newOption = new PortDefinition<BaseNode>(General.NewGuid(), "");
            data.Options.Add(newOption);
            SetOption(newOption);
        }

        private void HandleDeleteOption(ChoiceOptionForm form)
        {
            data.Options.Remove(form.portDefinition);
            form.RemoveFromHierarchy();
        }
        
        private void HandleUpdateActor(ChangeEvent<string> evt) => data.Actor = evt.newValue;

        private void HandleUpdateMessage(ChangeEvent<string> evt) => data.Message = evt.newValue;
    }
}