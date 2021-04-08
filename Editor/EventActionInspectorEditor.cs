using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Maz.Unity.EventFramework
{
    [CustomEditor(typeof(EventAction<>))]
    public class EventActionInspectorEditor : UnityEditor.Editor
    {
		public override void DrawPreview(Rect previewArea)
		{
			base.DrawPreview(previewArea);
		}
	}
}
