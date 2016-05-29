using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class PlayerStats : MonoBehaviour
    {
        public enum Effect { EquipCamera, EquipWheels }

        public Texture2D cursorImage;
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
        public float OverallCharge = 50;
        public float minSpeed;

        public Light sun;
        public float sunUp;
        public float sunDown;

        public List<RoverComponent> roverComponents = new List<RoverComponent>();
        public List<Recipe> knownRecipes = new List<Recipe>();

        public int movementEnabled = 0;
        private CursorLockMode desiredCursorLocking = CursorLockMode.Locked;

        private Inventory playerInventory = null;
        private TimeOfDay cachedTimeOfDay = null;
        

        private Inventory PlayerInventory
        {
            get { return (playerInventory != null) ? playerInventory : playerInventory = UIManager.GetMenu<Inventory>(); }
        }

        private TimeOfDay CachedTimeOfDay
        {
            //TODO remove gameobject.find
            get { return (cachedTimeOfDay != null) ? cachedTimeOfDay : cachedTimeOfDay = GameObject.FindObjectOfType<TimeOfDay>(); }
        }


        public void EnableMovement()
        {
            movementEnabled++;
        }


        public void DisableMovement()
        {
            movementEnabled--;
        }

        /// <summary>
        /// Add recipe to list of player's craftables
        /// </summary>
        /// <param name="r"></param>
        public void AddRecipe(Recipe r)
        {
            if (!knownRecipes.Contains(r)) knownRecipes.Add(r);
        }

        /// <summary>
        /// Remove recipe from list of player's craftables
        /// </summary>
        /// <param name="r"></param>
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

        /// <summary>
        /// Process stat, applying all stat modifications
        /// </summary>
        /// <param name="statId"></param>
        /// <returns></returns>
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


        public float MovementSpeed
        {
            get { return ModifyStat(MOVE_SPEED_ID) + minSpeed; }
        }


        public float CurrentMovementSpeed
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


        public float MaxCharge { get { return ModifyStat(MAX_Charge_ID); } }
        public float RechargeRate { get { return ModifyStat(RECHARGE_RATE_ID); } }
        public float HarvestSpeed { get { return ModifyStat(HARVEST_SPEED_ID); } }
        public float ScanningSpeed { get { return ModifyStat(SCANNING_SPEED_ID); } }
        public float MaxHealth { get { return ModifyStat(HEALTH_ID); } }
        public float Health { get { return OverallHealth; } }
        public float Weight { get { return ModifyStat(WEIGHT_ID) + PlayerInventory.Weight; } }


        public void ModifyHealth(float addedModifier)
        {
            OverallHealth += addedModifier;

            if(OverallHealth <= 0)
            {
                StartCoroutine(KillPlayer());
            }
        }

        /// <summary>
        /// Kill the player and begin the reset process
        /// </summary>
        /// <returns></returns>
        private IEnumerator KillPlayer()
        {
            Debug.Log("Killing Player!!");
            FadeMenu fm = UIManager.GetMenu<FadeMenu>();
            MessageMenu mm = UIManager.GetMenu<MessageMenu>();
            fm.Fade(0f, Color.clear, Color.black);
            mm.Open("Signal Lost", 3);

            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);
        }


        private void Update()
        {
#if UNITY_WEBGL
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.height / 2), CursorMode.Auto);
#else
            if (movementEnabled == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
                Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.height / 2), CursorMode.Auto);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width/2, cursorImage.height/2), CursorMode.Auto);
            }
#endif
        }


        public void FixedUpdate()
        {
            if (CachedTimeOfDay != null && sun.intensity > 0.75f)
            {
                //its light outside, charge the battery!
                if (OverallCharge < MaxCharge)
                {
                    OverallCharge += Time.fixedDeltaTime * RechargeRate * 0.25f;
                    UIManager.Close<DeadBatteryMenu>();
                }
            }
            else
            {
                //its dark outside, dont charge!
                if (OverallCharge > 0)
                {
                    OverallCharge -= Time.fixedDeltaTime * 0.75f;
                }
                else
                {
                    UIManager.Open<DeadBatteryMenu>();
                }
            }

            if((OverallCharge > MaxCharge * 0.001f && OverallCharge < MaxCharge * 0.1f) ||
                (OverallCharge > MaxCharge * 0.49f && OverallCharge < MaxCharge * 0.51f) ||
                (OverallCharge > MaxCharge * 0.89f && OverallCharge < MaxCharge * 0.91f))
            {
                UIManager.Open<ChargeTrackingMenu>();
            }
            else
            {
                UIManager.GetMenu<ChargeTrackingMenu>().Close();
            }
        }
    

        private void Awake()
        {
            Menu.OnMenuOpen += DisableMovement;
            Menu.OnMenuClose += EnableMovement;
        }
    }
}