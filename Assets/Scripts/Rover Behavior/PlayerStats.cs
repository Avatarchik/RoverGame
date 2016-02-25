using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class PlayerStats : MonoBehaviour
    {
        public enum Effect { EquipCamera, EquipWheels }

        public StatCollection statCollection;

        public const int MOVE_SPEED_ID = 0;
        public const int TURN_SPEED_ID = 1;
        public const int MAX_Charge_ID = 2;
        public const int RECHARGE_RATE_ID = 3;
        public const int HARVEST_SPEED_ID = 4;
        public const int SCANNING_SPEED_ID = 5;
        public const int HEALTH_ID = 6;
        public const int WEIGHT_ID = 7;

        public float OverallHealth;
        public float minSpeed;

        public List<RoverComponent> roverComponents = new List<RoverComponent>();
        public List<Recipe> knownRecipes = new List<Recipe>();

        public int movementEnabled = 0;
        private CursorLockMode desiredCursorLocking = CursorLockMode.Locked;

        private Inventory playerInventory = null;

        private Inventory PlayerInventory
        {
            get { return (playerInventory != null) ? playerInventory : playerInventory = UIManager.GetMenu<Inventory>(); }
        }


        public void EnableMovement()
        {
            movementEnabled++;
        }


        public void DisableMovement()
        {
            movementEnabled--;
        }


        public void AddRecipe(Recipe r)
        {
            if (!knownRecipes.Contains(r)) knownRecipes.Add(r);
        }


        public void RemoveRecipe(Recipe r)
        {
            for (int i = 0; i < knownRecipes.Count; i++)
            {
                if (r == knownRecipes[i])
                {
                    knownRecipes.RemoveAt(i);
                    break;
                }
            }
        }


        public float ModifyStat(int statId)
        {
            float val = statCollection.playerStats[statId].statValue;

            //look through all components for potential modifiers.
            foreach (RoverComponent rc in roverComponents)
            {
                //check if item is null or 'broken'
                if (rc != null && rc.equippedItem != null)
                {
                    //does it look like we have an item equipped thats supposed to modify stats?
                    if (rc.equippedItem.statModifiers.Count > 0)
                    {
                        foreach (Modifier mod in rc.equippedItem.statModifiers)
                        {
                            //should it modify THIS stat?
                            if (statId == mod.statId)
                            {
                                //yes
                                val += (mod.modifierValue * (rc.health / rc.maxHealth));
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
                    //Cursor.visible = false;
                    float speed = ModifyStat(MOVE_SPEED_ID) + minSpeed; /// (Weight * 0.01f)) + minSpeed;
                    return speed;
                }
                else
                {
                    //Cursor.lockState = CursorLockMode.None;
                    //Cursor.visible = true;
                    return 0f;
                }
            }
        }

        public float TurnSpeed
        {
            get
            {
                if (movementEnabled == 0)
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
            get { return ModifyStat(WEIGHT_ID) + PlayerInventory.Weight; }
        }


        public void ModifyHealth(float addedModifier)
        {
            OverallHealth += addedModifier;
        }


        private void Update()
        {
            if (movementEnabled == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }


        private void Awake()
        {
            Menu.OnMenuOpen += DisableMovement;
            Menu.OnMenuClose += EnableMovement;
        }
    }
}