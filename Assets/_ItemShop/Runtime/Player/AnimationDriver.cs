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
        List<SpriteAnimator> animators = new();

        [SerializeField]
        float animationVelocityMult = 1;

        public void Update()
        {
            if (!rb)
                return;

            foreach (var anim in animators)
            {
                Process(anim);
            }
        }

        private void Process(SpriteAnimator animator)
        {
            var vel = rb.velocity;
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
        }
    }
}
