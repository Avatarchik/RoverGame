using UnityEngine;
using System.Collections;

namespace Sol
{
    public class IntroTriggerArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                GameObject.FindObjectOfType<Intro>().Next();
                Destroy(this.gameObject);
            }
        }
    }
}
