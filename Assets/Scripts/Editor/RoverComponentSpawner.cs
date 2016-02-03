using UnityEngine;
using UnityEditor;

public class RoverComponentSpawner : MonoBehaviour
{


    [MenuItem("Assets/Create/Rover component")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<RoverComponent>();
    }
}
