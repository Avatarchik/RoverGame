using UnityEngine;
using UnityEditor;

public class SoundcollectionSpawner : MonoBehaviour {

    [MenuItem("Assets/Create/SoundCollection")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SoundCollection>();
    }
}
