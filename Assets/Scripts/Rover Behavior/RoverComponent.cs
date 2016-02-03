using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoverComponent : ScriptableObject
{
    public Item equippedItem;

    public float health = 100f;
    public float maxHealth = 100f;

    private float statValue;

 
    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (equippedItem == null)
            {
                health = 0;
                return;
            }                
        }
    }


    private void Awake()
    {
        Health = health;
    }

  
}
