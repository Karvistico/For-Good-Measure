using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Conversa.Runtime
{
    [MovedFrom(true, null, "Assembly-CSharp")]
    [Serializable]
    public class GroupData
    {
        [SerializeField] private string id;
        [SerializeField] private string title;
        [SerializeField] private Vector2 position;
        [SerializeReference] private List<string> elements;

        public string Id => id;
        
        public List<string> Elements => elements;
        
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }
        
        public string Title
        {
            get => title;
            set => title = value;
        }
        
        // Methods

        public GroupData(string title)
        {
            id = Guid.NewGuid().ToString();
            Title = title;
            elements = new List<string>();
        }

        public void AddElement(string element)
        {
            if (elements.Contains(element)) return;
            elements.Add(element);
        }

        public void RemoveElement(string element)
        {
            if (elements.Contains(element))
                elements.Remove(element);
        }

        public bool Contains(string element) => elements.Contains(element);
    }
}