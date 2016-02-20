using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Spawner : InteractibleObject
    {
        public Transform spawnPos;
        public List<RoverComponent> savedRoverComponents = new List<RoverComponent>();


        private PlayerStats player;


        public PlayerStats Player
        {
            get
            {

                return (player != null) ? player : player = GameObject.FindObjectOfType<PlayerStats>() as PlayerStats;
            }
            set
            {
                player = value;
            }
        }


        public override void Interact()
        {
            SaveRoverComponents();
            Debug.Log("rover components saved!");
        }


        public void Respawn()
        {
            //reset palyers position to respawn position
            Player.transform.position = spawnPos.position;

            //revert rover to last saved amount
            Player.roverComponents.Clear();
            Player.roverComponents.AddRange(savedRoverComponents);

            //clear out the player's inventory
            GameObject.FindObjectOfType<Inventory>().RemoveAllInventoryItems();
        }


        public void SaveRoverComponents()
        {
            savedRoverComponents.Clear();
            savedRoverComponents.AddRange(Player.roverComponents);
        }
    }
}