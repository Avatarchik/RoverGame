using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Stat
{
    public int id;
    public string displayName;

    public List<RoverComponent> equippedComponents = new List<RoverComponent>();

    [SerializeField]
    private float statValue;

    public float StatValue
    {
        get
        {
            float val = statValue;
            foreach (RoverComponent rc in equippedComponents)
            {
                if(rc.equippedItem != null)
                {
                    if(rc.equippedItem.statModifier != null)
                    {
                        //looks like we have an item equipped thats supposed to modify stats
                        if(id == rc.equippedItem.statModifier.statId)
                        {
                            //and its supposed to modify THIS stat
                            val += rc.ActualModifierValue;
                        }
                    }
                }
            }

            return val;
        }
    }
}
