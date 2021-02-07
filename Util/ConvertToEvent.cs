using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{
    public class ConvertToEvent : MonoBehaviour
    {

#pragma warning disable 169, 414
		// for Editor
		[SerializeField, HideInInspector]
		bool isOnRaisIntCollapsed = false, isOnRaiseFloatCollapsed = false, isOnRaiseDoubleCollapsed = false, isOnRaiseStringCollapsed = false;
#pragma warning restore 169, 414

		public UnityEvent<int> OnIntEvent;
        public UnityEvent<float> OnFloatEvent;
        public UnityEvent<double> OnDoubleEvent;
        public UnityEvent<string> OnStringEvent;

		public void ConvertIntToString(int value) => OnStringEvent?.Invoke(value.ToString());
		public void ConvertFloatToString(float value) => OnStringEvent?.Invoke(value.ToString());
		public void ConvertDoubleToString(double value) => OnStringEvent?.Invoke(value.ToString());


		public void ConvertIntToFloat(int value) => OnFloatEvent?.Invoke(value);
		public void ConvertIntToDouble(int value) => OnDoubleEvent?.Invoke(value);


		public void ConvertFloatToInt(float value) => OnIntEvent?.Invoke((int)value);
		public void ConvertFloatToDouble(float value) => OnDoubleEvent?.Invoke(value);
	}
}