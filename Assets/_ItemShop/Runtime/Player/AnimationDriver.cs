using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class AnimationDriver : MonoBehaviour
    {
        
        [SerializeField]
        Rigidbody2D rb;

        [SerializeField]
        CharacterAnimator animator;

        [SerializeField]
        float animationVelocityMult = 1;

        public void Update()
        {
            if (!rb || !animator)
                return;

            var vel = rb.velocity;

            //if (vel.magnitude < .1f)
            //{
            //    animator.SetCurrent("idle");
            //    return;
            //}
            animator.Pause = vel.magnitude < .01f;

            animator.SetSpeed(Mathf.Max(vel.magnitude * 2, 1));

            if (animator.Pause)
                return;

            var h = Vector2.Dot(vel, Vector2.left);
            if (h > .1f)
                animator.SetCurrent("left");
            else if (h < -.1f)
                animator.SetCurrent("right");
            
            var v = Vector2.Dot(vel, Vector2.up);
            if (v > .1f)
                animator.SetCurrent("up");
            else if (v < -.1f)
                animator.SetCurrent("down");

            //if (Mathf.Abs(vel.x) > .1f)
            //{
                
            //}
            //else
            //{
                
            //}
        }

    }
}
