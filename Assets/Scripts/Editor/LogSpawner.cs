using UnityEngine;
using UnityEditor;

public class LogSpawner : MonoBehaviour
{


    [MenuItem("Assets/Create/Log")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Log>();
    }
}
