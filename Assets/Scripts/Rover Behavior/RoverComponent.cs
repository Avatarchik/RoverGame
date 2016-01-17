using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoverComponent : MonoBehaviour
{
    public Item equippedItem;

    public float health = 100f;
    public float maxHealth = 100f;

    public Image componentImage;

    public Color goodColor = Color.green;
    public Color midColor = Color.yellow;
    public Color badColor = Color.red;

    public PlayerStats playerStats;

    private float statValue;


    public Color color
    {
        get
        {
            if (equippedItem == null) return badColor;

            if (Health / maxHealth > 0.5f)
            {
                return goodColor;
            }
            else if (health / maxHealth > 0.25f)
            {
                return midColor;
            }

            return badColor;
        }
    }


    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (equippedItem == null)
            {
                health = 0;
                componentImage.color = badColor;
                return;
            }                

            if (health/maxHealth > 0.5f)
            {
                componentImage.color = goodColor;
            }
            else if(health/maxHealth > 0.25f)
            {
                componentImage.color = midColor;
            }
            else if(health > 0)
            {
                componentImage.color = badColor;
            }
            else
            {
                equippedItem = null;
                componentImage.color = badColor;
            }
        }
    }


    private void Awake()
    {
        Health = health;
    }

  
}
