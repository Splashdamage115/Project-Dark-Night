using UnityEngine;

[CreateAssetMenu(fileName = "NewTextItem", menuName = "Custom/Text Item")]
public class TextItem : ScriptableObject
{
    public TextAsset textFile; // Drag & Drop the file here
    public string path; // Auto-generated file path (readonly in editor)
}

