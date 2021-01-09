using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{
    public class EventActionBase : ScriptableObject
    {



    }

    public class EventActionBase<T> : ScriptableObject
    {
        public UnityEvent<T> eventValue;

        public void Raise(T value)
        {
            eventValue.Invoke(value);
        }

        public void Register()
        {

		}
    }
}