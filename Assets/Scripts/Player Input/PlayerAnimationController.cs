using UnityEngine;
using System.Collections;

namespace Sol
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private const string ANIMATION_BOOL = "IsAlternateMovement";

        public PlayerStats playerStats;
        public EquipableItem newRoverWheels;
        public Animator anim;


        public Animator Anim
        {
            get { return (anim != null) ? anim : anim = GetComponentInChildren<Animator>(); }
        }


        public bool UseAlternateAnimation
        {
            get
            {
                foreach(RoverComponent rc in playerStats.roverComponents)
                {
                    if (rc.equippedItem == newRoverWheels) goto WheelsEquipped;
                }
                return (playerStats.movementEnabled == 0);

                WheelsEquipped:
                    return false;
            }
        }


        //TODO dont use update here
        public void Update()
        {
            Anim.SetBool(ANIMATION_BOOL, UseAlternateAnimation);
        }
    }
}

