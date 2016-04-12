using UnityEngine;
using System.Collections;

namespace Sol
{
    public class FallBehavior : MonoBehaviour
    {
        public PlayerStats player;
        public Rigidbody playerBody;

        public float fallDamageThreshhold = 5f;
        public float fallDamageMultiplier = 1.5f;

        private float yLastFrame;
        private float yThisFrame;

        private bool takeFallingDamage;
        private float maxFallSpeed;


        private void TakeFallingDamage(float velocity)
        {
            Debug.Log("taking damage! : " + -1 * velocity * fallDamageMultiplier);
            player.ModifyHealth(-1 * velocity * fallDamageMultiplier);
        }


        private void FixedUpdate()
        {
            float velocity = Mathf.Abs(playerBody.velocity.y);
            if (velocity > -0.1f && velocity < 0.1f) velocity = 0;

            if (!takeFallingDamage)
            {
                if (velocity > fallDamageThreshhold) takeFallingDamage = true;
            }
            else
            {
                if (velocity == 0)
                {
                    //we hit the ground, take the damage!
                    TakeFallingDamage(maxFallSpeed);
                    takeFallingDamage = false;
                }
                else
                {
                    //if (velocity <= fallDamageThreshhold) takeFallingDamage = false; //we slowed down in midair somehow?
                                                                                     //still falling, moar damage 
                    if (velocity > maxFallSpeed) maxFallSpeed = velocity;
                }
            }
        }

    }

}
