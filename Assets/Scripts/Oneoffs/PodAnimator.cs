using UnityEngine;
using System.Collections;

public class PodAnimator : MonoBehaviour
{
    public Animator animator;

    public GameObject door;
	
    public void OpenDoor()
    {
        door.SetActive(false);
    }
}
