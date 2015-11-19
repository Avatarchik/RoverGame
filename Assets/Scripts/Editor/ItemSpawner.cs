using UnityEngine;
using UnityEditor;

public class ItemSpawner : MonoBehaviour
{

    [MenuItem("Assets/Create/Item")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Item>();
    }
}
