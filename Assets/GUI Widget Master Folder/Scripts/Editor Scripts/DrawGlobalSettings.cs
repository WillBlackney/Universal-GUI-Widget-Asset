using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using BlackneyStudios.GuiWidget;

namespace BlackneStudios.Editor
{
    public class DrawGlobalSettings : DrawSceneObject<GlobalSettings>
    {
        // Directory reference to ALL the neccesary widget prefabs and scripts needed in the scene      
        private string installFilesPath = "Assets/GUI Widget Master Folder/Prefabs/All Widget Scene Objects.prefab";

        // Cached reference to the 'Global Settings' singleton in the scene
        [Title("Global Settings", "", TitleAlignments.Centered)]
        [ShowIf("@gs != null")]
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public GlobalSettings gs;
        public void FindGlobalSettingsSingletonInScene()
        {
            gs = GameObject.FindObjectOfType<GlobalSettings>();
        }

        // Auto Install Button + Logic
        [ShowIf("@gs == null")]
        [Button]
        [GUIColor(0.7f, 1f, 0.7f)]
        private void RunUniversalWidgetAssetSetup()
        {
            // Get the master prefab object from the project directory
            UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath(installFilesPath, typeof(GameObject));

            // Clone the prefab into a new instance, then rename it
            GameObject newController = GameObject.Instantiate(prefab) as GameObject;
            newController.name = "All Widget Scene Objects";

            // Cache the Global settings game object and script
            FindGlobalSettingsSingletonInScene();
        }

        // Auto selects the Global Settings singleton game object in the scene for the user.
        [ShowIf("@gs != null")]
        [GUIColor(0.7f, 1f, 0.7f)]
        [ButtonGroup("Top Button", -1000)]
        private void SelectGlobalSettingsInScene()
        {
            if (gs != null)
            {
                Selection.activeGameObject = gs.gameObject;
            }
        }

       


    }
}

