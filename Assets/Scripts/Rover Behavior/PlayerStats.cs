using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public enum Effect { EquipCamera, EquipWheels }

    public StatCollection statCollection;
    public Inventory playerInventory;

    public const int MOVE_SPEED_ID = 0;
    public const int TURN_SPEED_ID = 1;
    public const int MAX_Charge_ID = 2;
    public const int RECHARGE_RATE_ID = 3;
    public const int HARVEST_SPEED_ID = 4;
    public const int SCANNING_SPEED_ID = 5;
    public const int HEALTH_ID = 6;
    public const int WEIGHT_ID = 7;

    public float OverallHealth;

    public List<RoverComponent> roverComponents = new List<RoverComponent>();

    private int movementEnabled = 0;
    private CursorLockMode desiredCursorLocking;

    public void EnableMovement()
    {
        movementEnabled++;
        Debug.Log("movement enabled " + movementEnabled);
    }


    public void DisableMovement()
    {
        movementEnabled--;
        Debug.Log("movement enabled " + movementEnabled);
    }


    public float ModifyStat(int statId)
    {
        float val = statCollection.playerStats[statId].statValue;

        //look through all components for potential modifiers.
        foreach (RoverComponent rc in roverComponents)
        {
            //check if item is null or 'broken'
            if (rc.equippedItem != null)
            {
                //does it look like we have an item equipped thats supposed to modify stats?
                if (rc.equippedItem.statModifiers.Count > 0)
                {
                    foreach(Modifier mod in rc.equippedItem.statModifiers)
                    {
                        //should it modify THIS stat?
                        if (statId == mod.statId)
                        {
                            //yes
                            val += (mod.modifierValue * (rc.health/rc.maxHealth));
                        }
                    }
                }
            }
        }

        return val;
    }


    public float MoveSpeed
    {
        //TODO need to get this to be modified by weight.
        // - cant go below 0
        // - cant approach 0 too quickly
        get
        {
            if (movementEnabled == 0)
            {
                //Cursor.lockState = CursorLockMode.Locked;
               // Cursor.visible = false;
                return ModifyStat(MOVE_SPEED_ID) / (Weight * 0.01f);
            }
            else
            {
               // Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
                return 0f;
            }
        }
    }

    public float TurnSpeed
    {
        get
        {
            if(movementEnabled == 0)
            {
                return ModifyStat(TURN_SPEED_ID);
            }
            else
            {
                return 0f;
            }
        }
    }

    public float MaxCharge
    {
        get { return ModifyStat(MAX_Charge_ID); }
    }

    public float RechargeRate
    {
        get { return ModifyStat(RECHARGE_RATE_ID); }
    }

    public float HarvestSpeed
    {
        get { return ModifyStat(HARVEST_SPEED_ID); }
    }

    public float ScanningSpeed
    {
        get { return ModifyStat(SCANNING_SPEED_ID); }
    }

    public float MaxHealth
    {
        get { return ModifyStat(HEALTH_ID); }
    }

    public float Health
    {
        get { return OverallHealth; }
    }

    public float Weight
    {
        get { return ModifyStat(WEIGHT_ID) + playerInventory.Weight; }
    }


    public void ModifyHealth(float addedModifier)
    {
        OverallHealth += addedModifier;
    }


    private void Update()
    {
        Cursor.lockState = desiredCursorLocking;
    }


    private void Awake()
    {
        Menu.OnMenuOpen += DisableMovement;
        Menu.OnMenuClose += EnableMovement;
    }
}
