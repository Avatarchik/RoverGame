using UnityEngine;
using System.Collections;


public class Ingredient : ScriptableObject
{
    public int id;
    public string displayName;

    [TextArea]
    public string description;
    public Sprite image;

    public bool stackable = true;

    public float weight = 0.1f;
}