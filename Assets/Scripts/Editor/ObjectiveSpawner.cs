using UnityEngine;
using UnityEditor;

public class ObjectiveSpawner : MonoBehaviour
{


    [MenuItem("Assets/Create/Objective")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Objective>();
    }
}
