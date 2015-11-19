using UnityEngine;
using UnityEditor;
public class IngredientSpawner : MonoBehaviour
{

    [MenuItem("Assets/Create/Ingredient")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Ingredient>();
    }
}
