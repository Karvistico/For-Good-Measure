using Conversa.Runtime.Interfaces;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

namespace Conversa.Editor.Interfaces
{
    public interface INodeView
    {
        string Guid { get; }
        string GetPortId(Port port);
        UnityEvent OnBeforeChange { get; }

        bool GetPort(string guid, out Port port);
        void UpdateRect();
        void UpdateData();
        void SetPosition(Vector2 position);
        INode GetNode();
        Node GetGraphNode();
    }
}