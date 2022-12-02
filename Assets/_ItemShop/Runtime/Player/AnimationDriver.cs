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
        PlayerController playerController;

        [SerializeField]
        SpriteAnimator animator;

        [SerializeField]
        float animationVelocityMult = 1;

        bool lastMovementEnabled = true;

        public void Update()
        {
            if (!rb)
                return;

            Process(animator);
        }

        private void Process(SpriteAnimator animator)
        {
            var vel = rb.velocity;
            animator.Pause = vel.magnitude < .01f;

            animator.SetSpeed(Mathf.Max(vel.magnitude * 2, 1));

            if (lastMovementEnabled != playerController.MovementEnabled)
            {
                lastMovementEnabled = playerController.MovementEnabled;
                animator.SetCurrent("down");
                animator.RefreshSprites();
            }

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
        }
    }
}
