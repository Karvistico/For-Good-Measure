using System;
using Conversa.Editor;
using Conversa.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Conversa.Runtime.Nodes.MathOperators
{
	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Out", "out", typeof(float), Flow.Out, Capacity.Many)]
	public class AddNode : BaseNode, IValueNode
	{
		[SerializeField] private float inA;
		[SerializeField] private float inB;
		
		[Slot("A", "in-a", Flow.In, Capacity.One)]
		public float InA
		{
			get => inA;
			set => inA = value;
		}
		
		[Slot("B", "in-b", Flow.In, Capacity.One)]
		public float InB
		{
			get => inB;
			set => inB = value;
		}

		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var value1 = conversation.IsConnected(Guid, "in-a")
				? conversation.GetConnectedValueTo<float>(this, "in-a")
				: inA;

			var value2 = conversation.IsConnected(Guid, "in-b")
				? conversation.GetConnectedValueTo<float>(this, "in-b")
				: inB;

			var output = value1 + value2;

			return Converter.ConvertValue<float, T>(output);
		}
	}

	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Out", "out", typeof(float), Flow.Out, Capacity.Many)]
	public class SubtractNode : BaseNode, IValueNode
	{
		[SerializeField] private float inA;
		[SerializeField] private float inB;
		
		[Slot("A", "in-a", Flow.In, Capacity.One)]
		public float InA
		{
			get => inA;
			set => inA = value;
		}
		
		[Slot("B", "in-b", Flow.In, Capacity.One)]
		public float InB
		{
			get => inB;
			set => inB = value;
		}
		
		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var value1 = conversation.IsConnected(Guid, "in-a")
				? conversation.GetConnectedValueTo<float>(this, "in-a")
				: inA;

			var value2 = conversation.IsConnected(Guid, "in-b")
				? conversation.GetConnectedValueTo<float>(this, "in-b")
				: inB;


			var output = value1 - value2;

			return Converter.ConvertValue<float, T>(output);
		}
	}

	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Out", "out", typeof(float), Flow.Out, Capacity.Many)]
	public class MultiplyNode : BaseNode, IValueNode
	{
		[SerializeField] private float inA;
		[SerializeField] private float inB;
		
		[Slot("A", "in-a", Flow.In, Capacity.One)]
		public float InA
		{
			get => inA;
			set => inA = value;
		}
		
		[Slot("B", "in-b", Flow.In, Capacity.One)]
		public float InB
		{
			get => inB;
			set => inB = value;
		}
		
		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var value1 = conversation.IsConnected(Guid, "in-a")
				? conversation.GetConnectedValueTo<float>(this, "in-a")
				: inA;

			var value2 = conversation.IsConnected(Guid, "in-b")
				? conversation.GetConnectedValueTo<float>(this, "in-b")
				: inB;


			var output = value1 * value2;

			return Converter.ConvertValue<float, T>(output);
		}
	}

	[MovedFrom(true, null, "Assembly-CSharp")]
	[Serializable]
	[Port("Out", "out", typeof(float), Flow.Out, Capacity.Many)]
	public class DivideNode : BaseNode, IValueNode
	{
		[SerializeField] private float inA;
		[SerializeField] private float inB;
		
		[Slot("A", "in-a", Flow.In, Capacity.One)]
		public float InA
		{
			get => inA;
			set => inA = value;
		}
		
		[Slot("B", "in-b", Flow.In, Capacity.One)]
		public float InB
		{
			get => inB;
			set => inB = value;
		}
		
		public T GetValue<T>(string portGuid, Conversation conversation)
		{
			if (portGuid != "out") return default;

			var value1 = conversation.IsConnected(Guid, "in-a")
				? conversation.GetConnectedValueTo<float>(this, "in-a")
				: inA;

			var value2 = conversation.IsConnected(Guid, "in-b")
				? conversation.GetConnectedValueTo<float>(this, "in-b")
				: inB;

			var output = value1 / value2;

			return Converter.ConvertValue<float, T>(output);
		}
	}
}