#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using BlackneyStudios.GuiWidget;
using System;

namespace BlackneStudios.Editor
{
    public class WidgetMenuManager : OdinMenuEditorWindow
    {
        [OnValueChanged("StateChange")]
        private ManagerState managerState;
        private int enumIndex = 0;

        // Create field for each type of manager object in project to be drawn
        private DrawGlobalSettings drawGlobalSettings = new DrawGlobalSettings();

        // Create a drop down menu in the Unity editor top panel.
        [MenuItem("Tools/BlackneyStudios/Universal Widget")]
        public static void OpenWindow()
        {
            GetWindow<WidgetMenuManager>().Show();
        }
        // Does global settings already exist in scene?
        protected override void Initialize()
        {
            drawGlobalSettings.FindGlobalSettingsSingletonInScene();
        }
        protected override void OnGUI()
        {
            // Load the asset logo
            Texture2D logo = (Texture2D)AssetDatabase.LoadAssetAtPath
                ("Assets/GUI Widget Master Folder/Resources/uw_logo_1.png", typeof(Texture2D));

            // Create a nice title for the menu screen
            SirenixEditorGUI.Title("Universal Widget", "Blackney Studios", TextAlignment.Center, true);
            EditorGUILayout.Space();

            // did we correctly load the logo image?
            if (logo)
            {
                // We did, draw the logo below the title
                GUILayout.BeginHorizontal("box");
                GUILayout.BeginArea(new Rect(16, 24, 80, 30));
                GUILayout.EndArea();
                GUILayout.FlexibleSpace();
                GUILayout.Label(logo);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.Space();
            // Finished doing Odin related GUI stuff, safe to call Unity's native OnGUI stuff now
            base.OnGUI();
        }
        protected override void DrawEditors()
        {
            // Which target should the window draw?
            switch (managerState)
            {
                case ManagerState.globalSettings:
                    DrawEditor(enumIndex);
                    break;
            }

            // Cast the enum value as int to be used as the index
            DrawEditor((int)managerState);
        }
        protected override IEnumerable<object> GetTargets()
        {
            List<object> targets = new List<object>();  
            targets.Add(drawGlobalSettings);
            targets.Add(base.GetTarget());
            enumIndex = targets.Count - 1;
            return targets;
        }        
        protected override void DrawMenu()
        {            
            switch (managerState)
            {
                default:
                    break;                
            }        
        }               
        protected override OdinMenuTree BuildMenuTree()
        {
            return new OdinMenuTree();
        }
        
        public enum ManagerState
        {
            globalSettings,
        };

    }


}




#endif

