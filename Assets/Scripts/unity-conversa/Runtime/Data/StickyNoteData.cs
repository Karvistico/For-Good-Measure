using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Conversa.Runtime
{
	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	public class StickyNoteData
	{
		[SerializeField] private string id;
		[SerializeField] private Rect rect;
		[SerializeField] private string title;
		[SerializeField] private string contents;

		public string Id => id;
		public Rect Rect { get => rect; set => rect = value; }
		public string Title { get => title; set => title = value; }
		public string Contents { get => contents; set => contents = value; }

		public StickyNoteData()
		{
			id = Guid.NewGuid().ToString();
		}

		public StickyNoteData(Vector2 position)
		{
			id = Guid.NewGuid().ToString();
			Rect = new Rect(position, new Vector2(100, 100));
			Title = "New title";
			Contents = "New content";
		}
	}
}