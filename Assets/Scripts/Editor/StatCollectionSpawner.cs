using UnityEngine;
using UnityEditor;

public class StatCollectionSpawner : MonoBehaviour
{

    [MenuItem("Assets/Create/Stat Collection")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<StatCollection>();
    }
}
