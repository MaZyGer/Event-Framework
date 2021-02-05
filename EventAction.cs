using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{
	public abstract class EventAction<T> : ScriptableObject
	{
		private List<EventListener<T>> listeners = new List<EventListener<T>>();


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
	}

	[CreateAssetMenu(menuName = ScriptableEventConstants.MenuName + "/" + nameof(EventAction))]
	public class EventAction : ScriptableObject
	{
		#region Events
		[UnityEventCollapse]
		public UnityEvent GameEvent;

		[UnityEventCollapse]
		public UnityEvent<int> IntEvent;

		[UnityEventCollapse]
		public UnityEvent<float> FloatEvent;

		[UnityEventCollapse]
		public UnityEvent<double> DoubleEvent;

		[UnityEventCollapse]
		public UnityEvent<string> StringEvent;
		#endregion


		private List<EventListener> listeners = new List<EventListener>();

		//public void Raise<T>(T value)
		//{
		//	for (int i = listeners.Count - 1; i >= 0; i--)
		//	{
		//		//listeners[i].OnEventRaised<T>(value);
		//	}
		//}

		#region Raise

		public void Raise<T>(T value)
		{
			switch (value)
			{
				case int v:
					Raise(v);
					break;
				case float v:
					Raise(v);
					break;
				case double v:
					Raise(v);
					break;
				case string v:
					Raise(v);
					break;
				default:
					throw new System.TypeAccessException("Cannot execute raise. No type of " + typeof(T) + " found");
			}
		}
		public void Raise()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised();
			}
		}

		public void Raise(int value)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}

		public void Raise(float value)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}

		public void Raise(double value)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}

		public void Raise(string value)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
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
	}
}

