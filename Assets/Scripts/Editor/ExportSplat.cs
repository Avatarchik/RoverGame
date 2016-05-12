using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class ExportSplat : EditorWindow
{
    // For saving splat map as PNG file. 

    [MenuItem("Terrain/Export Texture...")]
    static void Apply()
    {
        Texture2D texture  = Selection.activeObject as Texture2D;
        if (texture == null)
        {
            EditorUtility.DisplayDialog("Select Texture", "You Must Select a Texture first!", "Ok");
            return;
        }

        var bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/exported_texture.png", bytes);
    }
}
