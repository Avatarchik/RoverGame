using UnityEngine;
using UnityEditor;

namespace Sol
{
    public class EquippableItemSpawner : MonoBehaviour
    {
        [MenuItem("Assets/Create/Equippable Item")]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<EquipableItem>();
        }
    }
}