using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    [System.Serializable]
    public class RoverComponent
    {
        public enum ComponentType
        {
            Engine,
            Camera,
            Panels,
            Wheels,
            Antenna,
            Chassi,
            Battery,
            Drill,
            Scanner
        }

        public ComponentType currentComponentType;
        public EquipableItem equippedItem;

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
}

