using System.IO;
using UnityEngine;

public class ReadTextFile : MonoBehaviour
{
    // Reads the entire file as a string
    public static string readFile(string path)
    {
        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        else
        {
            Debug.LogError($"File not found: {path}");
            return string.Empty;
        }
    }

    // Reads a specified number of lines from the file
    public static string readFile(string path, int lineCount)
    {
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            int linesToRead = Mathf.Min(lineCount, lines.Length);
            return string.Join("\n", lines, 0, linesToRead);
        }
        else
        {
            Debug.LogError($"File not found: {path}");
            return string.Empty;
        }
    }
}
