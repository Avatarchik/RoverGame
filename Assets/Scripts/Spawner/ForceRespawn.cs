using UnityEngine;
using System.Collections;

public class ForceRespawn : MonoBehaviour
{
    public Spawner spawner;

    public void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Player") spawner.Respawn();
    }
}
