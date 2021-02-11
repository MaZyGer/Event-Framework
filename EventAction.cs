using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{
	public interface IRaiser
	{
		void Raise();
	}

	public interface IRaiser<T>
	{
		internal void Raise(T value);
	}

	public abstract class EventActionBase : ScriptableObject, IRaiser
	{
		public abstract void Raise();
	}

	public abstract class EventAction<T> : EventActionBase, IRaiser<T>
	{
		List<EventListener<T>> listeners = new List<EventListener<T>>();

		#region Raise
		internal void Raise(T value)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}
		#endregion

		public void RegisterListener(EventListener<T> listener)
		{
			listeners.Add(listener);
		}

		public void UnregisterListener(EventListener<T> listener)
		{
			listeners.Remove(listener);
		}

		#region Events
		[UnityEventCollapse]
		public UnityEvent<T> Event;
		#endregion

		[SerializeField, HideInInspector]
		T initialValue;

		[SerializeField, HideInInspector]
		T runtimeValue;
		public T Value
		{
			get => runtimeValue;
			set
			{
				if (this.runtimeValue == null || !this.runtimeValue.Equals(value))
				{
					this.runtimeValue = value;
					changed = true;
					Raise(Value);
				}
			}
		}

		bool changed;
		public bool Changed
		{
			get
			{
				bool retValue = changed;

				if (changed)
					changed = false;

				return retValue;
			}

		}

		public override void Raise()
		{
			Raise(Value);
		}

		private void OnDisable()
		{
			runtimeValue = initialValue;
			//Debug.Log($"<color=#FF0000FF>OnDisable</color> - new Value {runtimeValue}");
		}

		void IRaiser<T>.Raise(T value)
		{
			Raise(value);
		}
	}
}

