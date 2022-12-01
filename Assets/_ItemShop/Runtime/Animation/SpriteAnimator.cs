using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemShop
{
    public class SpriteAnimator : MonoBehaviour
    {

        [Serializable]
        struct Item
        {
            public string name;
            public SpriteAnimation anim;
        }

        class AnimationSet
        {
            List<(SpriteAnimation anim, SpriteRenderer render)> overlays = new();

            public AnimationSet(SpriteAnimation anim)
            {
                Main = anim;
            }
            public SpriteAnimation Main { get; set; }
            
            public int OverlayCount => overlays.Count;

            public void AddOverlay(SpriteAnimation anim, SpriteRenderer render)
            {
                overlays.Add((anim, render));
            }
            public void RemoveOverlay(int index)
            {
                overlays.RemoveAt(index);
            }

            public void SetOverlaySprite(int o, int index)
            {
                if (o >= overlays.Count || index >= overlays[o].anim.Keys.Count)
                    return;

                overlays[o].render.sprite = overlays[o].anim.Keys[index];
            }

        }

        [SerializeField]
        List<Item> anims = new();

        [SerializeField]
        SpriteRenderer spriteRenderer;

        //[SerializeField]
        //List<SpriteRenderer> overlays = new();

        Dictionary<string, AnimationSet> animations;

        AnimationSet current;
        int currentIndex;

        //float Duration => current.Keys.Count / current.FramesPerSecond;
        
        [field: SerializeField, Range(.1f, 5)]
        public float SpeedMult { get; private set; } = 1;
        public bool Pause { get; set; }

        public void SetSpeed(float speed)
        {
            SpeedMult = Mathf.Max(speed, .1f);
        }

        public void SetCurrent(string animation)
        {
            if (animations.ContainsKey(animation))
            {
                current = animations[animation];
            }
        }

        /// <summary>
        /// Create an Overlay, an extra animation to play in another Sprite Renderer
        /// </summary>
        /// <param name="name"> Name of the Animation </param>
        /// <param name="anim"> Animation to Play </param>
        /// <param name="target"> SpriteRenderer to play it on </param>
        public void CreateOverlay(string name, SpriteAnimation anim, SpriteRenderer target)
        {
            if (!animations.ContainsKey(name))
                return;

            animations[name].AddOverlay(anim, target);
        }
        
        private void SetSprites(int index)
        {
            spriteRenderer.sprite = current.Main.Keys[index];

            for (int i = 0; i < current.OverlayCount; i++)
            {
                //if (overlays.Count >= i)
                //    break;
                //if (index > current.Overlays[i].anim.Keys.Count)
                //    continue;

                current.SetOverlaySprite(i, index);

                //overlays[i].sprite = current.Overlays[i].Keys[index];
            }
        }

        #region Unity

        private void Awake()
        {
            animations = new(anims.Count);
            foreach (var item in anims)
            {
                animations.Add(item.name, new AnimationSet(item.anim));
            }

            if (anims.Count > 0)
                current = animations.First().Value;
        }
        IEnumerator Start()
        {
            while (true)
            {
                if (current == null)
                {
                    yield return new WaitUntil(() => current != null);
                }
                if (Pause)
                {
                    currentIndex = 0;
                    SetSprites(currentIndex);
                    yield return new WaitUntil(() => !Pause);
                }

                yield return new WaitForSeconds(current.Main.Delay / SpeedMult);

                currentIndex++;
                if (currentIndex >= current.Main.Keys.Count)
                    currentIndex = 0;

                SetSprites(currentIndex);
            }
        }

        #endregion
    }
}
