using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextItem))]
public class TextItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get reference to the target ScriptableObject
        TextItem textItem = (TextItem)target;

        // Draw the default inspector UI
        DrawDefaultInspector();

        // Ensure there is a text file assigned
        if (textItem.textFile != null)
        {
            // Get the asset path in Unity
            string assetPath = AssetDatabase.GetAssetPath(textItem.textFile);

            // Convert to system file path if needed
            textItem.path = assetPath;

            // Mark as dirty to save changes
            EditorUtility.SetDirty(textItem);
        }
    }
}
