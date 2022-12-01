using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class ClothesManager : MonoBehaviour
    {
        

        [SerializeField]
        SpriteAnimator animator;

        [Serializable]
        struct Item
        {
            public SpriteRenderer render;
            public SpriteAnimation up, down, left, right;
        }

        [SerializeField]
        List<Item> items;

        private void Start()
        {
            foreach (var item in items)
            {
                animator.CreateOverlay("up", item.up, item.render);
                animator.CreateOverlay("down", item.down, item.render);
                animator.CreateOverlay("left", item.left, item.render);
                animator.CreateOverlay("right", item.right, item.render);
            }
        }
    }
}
