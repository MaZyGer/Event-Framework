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

	[CreateAssetMenu(menuName = ScriptableEventConstants.MenuName + "/" + nameof(EventAction) + "(Void)")]
	public class EventAction : EventActionBase
	{
		List<EventListener> listeners = new List<EventListener>();

		#region Raise
		public void Raise()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised();
			}

			Event?.Invoke();
		}
		#endregion

		public void RegisterListener(EventListener listener)
		{
			listeners.Add(listener);
		}

		public void UnregisterListener(EventListener listener)
		{
			listeners.Remove(listener);
		}

		#region Events
		[UnityEventCollapse]
		public UnityEvent Event;
		#endregion
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

			Event?.Invoke(value);
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

