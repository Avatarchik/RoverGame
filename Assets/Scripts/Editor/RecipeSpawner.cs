using UnityEngine;
using UnityEditor;

public class RecipeSpawner : MonoBehaviour
{

    [MenuItem("Assets/Create/Recipe")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Recipe>();
    }
}
