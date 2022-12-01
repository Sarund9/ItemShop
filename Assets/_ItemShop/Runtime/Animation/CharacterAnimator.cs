using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class CharacterAnimator : MonoBehaviour
    {

        [Serializable]
        struct Item
        {
            public CharacterAnimation anim;
            public string name;
        }

        [SerializeField]
        List<Item> anims = new();

        [SerializeField]
        SpriteRenderer spriteRenderer;

        Dictionary<string, CharacterAnimation> animations;

        CharacterAnimation current;
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


        #region Unity

        private void Awake()
        {
            animations = new(anims.Count);
            foreach (var item in anims)
            {
                animations.Add(item.name, item.anim);
            }

            if (anims.Count > 0)
                current = anims[0].anim;
        }
        IEnumerator Start()
        {
            while (true)
            {
                if (!current)
                {
                    yield return new WaitUntil(() => current);
                }
                if (Pause)
                {
                    currentIndex = 0;
                    spriteRenderer.sprite = current.Keys[currentIndex];
                    yield return new WaitUntil(() => !Pause);
                }

                yield return new WaitForSeconds(current.Delay / SpeedMult);

                currentIndex++;
                if (currentIndex >= current.Keys.Count)
                    currentIndex = 0;

                spriteRenderer.sprite = current.Keys[currentIndex];
            }
        }

        #endregion
    }
}
