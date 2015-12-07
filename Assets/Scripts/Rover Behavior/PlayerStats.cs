using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public enum Effect { EquipCamera, EquipWheels }

    public int MOVE_SPEED_ID = 0;
    public int TURN_SPEED_ID = 1;
    public int MAX_Charge_ID = 2;
    public int RECHARGE_RATE_ID = 3;
    public int HARVEST_SPEED_ID = 4;
    public int SCANNING_SPEED_ID = 5;
    public int HEALTH_ID = 6;


    public List<Stat> stats = new List<Stat> ();

    public List<RoverComponent> roverComponents = new List<RoverComponent>();
}
