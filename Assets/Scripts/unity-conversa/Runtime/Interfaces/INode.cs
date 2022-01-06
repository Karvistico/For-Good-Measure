using UnityEngine;

namespace Conversa.Runtime.Interfaces
{
    public interface INode
    {
        string Guid { get; }
        public Rect NodeRect { get; set; }

        bool IsValid(Conversation conversation);
        NodePort GetNodePort(string portGuid);
        bool ContainsPort(string portGuid, Flow flow);
    }
}