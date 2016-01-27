using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private PlayerStats playerStats;


    public PlayerStats Stats
    {
        get { return (playerStats != null) ? playerStats : playerStats = GameObject.FindObjectOfType<PlayerStats>(); }
    }
}
