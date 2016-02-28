using UnityEngine;
using System.Collections;

namespace Sol
{
    public class PodDoor : Door
    {
        public PodAnimator podAnimator;

        public override void OpenDoor()
        {
            podAnimator.OpenDoor();
        }


        public override void CloseDoor()
        {

        }
    }
}