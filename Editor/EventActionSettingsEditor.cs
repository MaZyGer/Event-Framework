using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Maz.Unity.EventFramework
{
    [InitializeOnLoad]
    public class EventFrameworkSettings
    {
        const string AllowRaiseInEditmodePrefkey = nameof(Maz.Unity.EventFramework) + "AllowRaiseInEditmode";
        public static bool AllowRaiseInEditMode = false;

        static EventFrameworkSettings()
        {
            AllowRaiseInEditMode = EditorPrefs.GetBool(AllowRaiseInEditmodePrefkey, AllowRaiseInEditMode);
        }
         
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider() 
        {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Project Settings window.
            var provider = new SettingsProvider("Project/" + nameof(EventFrameworkSettings), SettingsScope.Project)
            {
                // By default the last token of the path is used as display name if no label is provided.
                label = "EventFramework",
                // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
                guiHandler = (searchContext) =>
                {
                    EditorGUI.BeginChangeCheck();
                    AllowRaiseInEditMode = EditorGUILayout.ToggleLeft(new GUIContent("Enable Raise button in Editmode"), AllowRaiseInEditMode);
                    

                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorPrefs.SetBool(AllowRaiseInEditmodePrefkey, AllowRaiseInEditMode);
                    }
                }, 

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(new[] { "Number", "Some String" })
            };

            return provider;
        }
    }
   
}