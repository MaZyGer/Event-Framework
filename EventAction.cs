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
		void Raise(T value);
	}

	public abstract class EventActionBase : ScriptableObject
	{
	
	}

	public abstract class EventAction<T> : EventActionBase, IRaiser<T>
	{
		List<EventListener<T>> listeners = new List<EventListener<T>>();

		#region Raise
		public void Raise(T value)
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
	}
}

