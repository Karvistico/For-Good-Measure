using System;
using Conversa.Editor;
using Conversa.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Conversa.Runtime
{
	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Out", "out", typeof(bool), Flow.Out, Capacity.Many)]
	public class AndNode : BaseNode, IValueNode
	{
		[SerializeField] private bool inA;
		[SerializeField] private bool inB;

		[Slot("A", "in-a", Flow.In, Capacity.One)]
		public bool InA
		{
			get => inA;
			set => inA = value;
		}
		
		[Slot("B", "in-b", Flow.In, Capacity.One)]
		public bool InB
		{
			get => inB;
			set => inB = value;
		}

		public override bool IsValid(Conversation conversation) => true;

		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var condition1 = conversation.IsConnected(Guid, "in-a")
				? conversation.GetConnectedValueTo<bool>(this, "in-a")
				: inA;

			var condition2 = conversation.IsConnected(Guid, "in-b")
				? conversation.GetConnectedValueTo<bool>(this, "in-b")
				: inB;
			
			var output = condition1 && condition2;

			return Converter.ConvertValue<bool, T>(output);
		}

	}

	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Out", "out", typeof(bool), Flow.Out, Capacity.Many)]
	public class NorNode : BaseNode, IValueNode
	{
		[SerializeField] private bool inA;
		[SerializeField] private bool inB;
		
		[Slot("A", "in-a", Flow.In, Capacity.One)]
		public bool InA
		{
			get => inA;
			set => inA = value;
		}
		
		[Slot("B", "in-b", Flow.In, Capacity.One)]
		public bool InB
		{
			get => inB;
			set => inB = value;
		}

		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var condition1 = conversation.IsConnected(Guid, "in-a")
				? conversation.GetConnectedValueTo<bool>(this, "in-a")
				: inA;

			var condition2 = conversation.IsConnected(Guid, "in-b")
				? conversation.GetConnectedValueTo<bool>(this, "in-b")
				: inB;
			
			var output = !(condition1 || condition2);
			
			return Converter.ConvertValue<bool, T>(output);
		}
	}

	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("In", "in", typeof(bool), Flow.In, Capacity.One)]
	[Port("Out", "out", typeof(bool), Flow.Out, Capacity.Many)]
	public class NotNode : BaseNode, IValueNode
	{
		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var input = conversation.GetConnectedValueTo<bool>(this, "in");
			var output = !input;

			return Converter.ConvertValue<bool, T>(output);
		}
	}

	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Out", "out", typeof(bool), Flow.Out, Capacity.Many)]
	public class OrNode : BaseNode, IValueNode
	{
		[SerializeField] private bool inA;
		[SerializeField] private bool inB;
		
		[Slot("A", "in-a", Flow.In, Capacity.One)]
		public bool InA
		{
			get => inA;
			set => inA = value;
		}
		
		[Slot("B", "in-b", Flow.In, Capacity.One)]
		public bool InB
		{
			get => inB;
			set => inB = value;
		}

		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var condition1 = conversation.IsConnected(Guid, "in-a")
				? conversation.GetConnectedValueTo<bool>(this, "in-a")
				: inA;

			var condition2 = conversation.IsConnected(Guid, "in-b")
				? conversation.GetConnectedValueTo<bool>(this, "in-b")
				: inB;
			
			var output = condition1 || condition2;

			return Converter.ConvertValue<bool, T>(output);
		}
	}

	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Out", "out", typeof(bool), Flow.Out, Capacity.Many)]
	public class XorNode : BaseNode, IValueNode
	{
		[SerializeField] private bool inA;
		[SerializeField] private bool inB;
		
		[Slot("A", "in-a", Flow.In, Capacity.One)]
		public bool InA
		{
			get => inA;
			set => inA = value;
		}
		
		[Slot("B", "in-b", Flow.In, Capacity.One)]
		public bool InB
		{
			get => inB;
			set => inB = value;
		}

		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var condition1 = conversation.IsConnected(Guid, "in-a")
				? conversation.GetConnectedValueTo<bool>(this, "in-a")
				: inA;

			var condition2 = conversation.IsConnected(Guid, "in-b")
				? conversation.GetConnectedValueTo<bool>(this, "in-b")
				: inB;
			
			var output = condition1 ^ condition2;

			return Converter.ConvertValue<bool, T>(output);
		}
	}
}