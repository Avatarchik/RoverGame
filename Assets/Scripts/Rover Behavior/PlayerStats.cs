using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public enum Effect { EquipCamera, EquipWheels }

    public int MOVE_SPEED_ID = 0;
    public int TURN_SPEED_ID = 1;

    public List<Stat> stats = new List<Stat> ();

    public List<RoverComponent> roverComponents = new List<RoverComponent>();
}
