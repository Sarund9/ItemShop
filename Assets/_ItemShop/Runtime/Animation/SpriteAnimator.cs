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
            public SpriteAnimation anim;
            public string name;
        }

        //class AnimationSet
        //{
        //    public AnimationSet(CharacterAnimation anim)
        //    {
        //        Main = anim;
        //    }
        //    public CharacterAnimation Main { get; set; }
        //    public List<CharacterAnimation> Overlays { get; } = new();
        //}

        [SerializeField]
        List<Item> anims = new();

        [SerializeField]
        SpriteRenderer spriteRenderer;

        [SerializeField]
        List<SpriteRenderer> overlays = new();

        Dictionary<string, SpriteAnimation> animations;

        SpriteAnimation current;
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

        public void SetAnimation(string name, SpriteAnimation anim)
        {
            animations[name] = anim;
        }
        public bool RemoveAnimation(string name) =>
            animations.Remove(name);

        private void SetSprites(int index)
        {
            spriteRenderer.sprite = current.Keys[index];
        }

        #region Unity

        private void Awake()
        {
            animations = new(anims.Count);
            foreach (var item in anims)
            {
                animations.Add(item.name, item.anim);
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

                yield return new WaitForSeconds(current.Delay / SpeedMult);

                currentIndex++;
                if (currentIndex >= current.Keys.Count)
                    currentIndex = 0;

                SetSprites(currentIndex);
            }
        }

        #endregion
    }
}
