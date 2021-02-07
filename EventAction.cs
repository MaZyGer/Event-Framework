using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{
	public abstract class EventActionBase : ScriptableObject
	{
		public abstract void Raise();
	}


	public abstract class EventActionBase<T> : EventActionBase
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

	}

	public abstract class EventAction<T> : EventActionBase<T>
	{
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
	}
}

