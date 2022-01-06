using System;
using Conversa.Editor;
using Conversa.Editor.Utils;
using Conversa.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Conversa.Runtime
{
    [MovedFrom(true, null, "Assembly-CSharp")]
    [Serializable]
    [Port("A", "in-a", typeof(float), Flow.In, Capacity.One)]
    [Port("B", "in-b", typeof(float), Flow.In, Capacity.One)]
    [Port("Out", "out", typeof(bool), Flow.Out, Capacity.Many)]
    public class CompareNode : BaseNode, IValueNode
    {
        public enum CompareOperation
        {
            Greater,
            GreatOrEqual,
            Less,
            LessOrEqual,
            Equals,
            Different
        }

        [SerializeField] private CompareOperation operation;

        public CompareOperation Operation {
            get => operation;
            set => operation = value;
        }

        public T GetValue<T>(string portGuid, Conversation conversation)
        {
            if (portGuid != "out") return default;

            var value1 = conversation.GetConnectedValueTo<float>(this, "in-a");
            var value2 = conversation.GetConnectedValueTo<float>(this, "in-b");
            
            var output = operation switch
            {
                CompareOperation.Greater => value1 > value2,
                CompareOperation.GreatOrEqual => value1 >= value2,
                CompareOperation.Less => value1 < value2,
                CompareOperation.LessOrEqual => value1 <= value2,
                CompareOperation.Equals => value1 == value2,
                CompareOperation.Different => value1 != value2,
                _ => false
            };

            return Converter.ConvertValue<bool, T>(output);
        }
    }}