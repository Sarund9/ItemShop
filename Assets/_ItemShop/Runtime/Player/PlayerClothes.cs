using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class PlayerClothes : MonoBehaviour, IContainer
    {

        [SerializeField]
        SpriteAnimator animator;

        [Serializable]
        struct ItemSlot
        {
            public SpriteRenderer render;
            public ItemCategory category;
        }

        [SerializeField]
        List<ItemSlot> slots;

        [SerializeField]
        Item[] underwear;

        [SerializeField]
        Item[] items;

        public event Action<int> OnItemChanged;

        public int Width => 1;

        public int Height => 4;

        public Item this[int slot]
        {
            get => items[slot];
        }

        public bool TrySet(int slot, Item item)
        {
            items[slot] = item;
            UpdateView(slot, item);
            return true;
        }

        void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            var old = items;
            items = new Item[Width * Height];
            Array.Copy(old, items, Mathf.Min(old.Length, items.Length));

        }

        private void Start()
        {
            for (int i = 0; i < Mathf.Min(underwear.Length, slots.Count); i++)
            {
                Item under = underwear[i];
                var slot = slots[i];

                if (!under || !slot.render)
                    continue;
                AssignAnimations(under, slot);
            }
            
            for (int i = 0; i < Mathf.Min(items.Length, slots.Count); i++)
            {
                if (items[i])
                    AssignAnimations(items[i], slots[i]);
            }
        }

        private void AssignAnimations(Item item, ItemSlot slot)
        {
            if (!slot.render)
                return;
            
            if (item)
            {
                animator.SetOverlay("up", item.UpAnim, slot.render);
                animator.SetOverlay("down", item.DownAnim, slot.render);
                animator.SetOverlay("left", item.LeftAnim, slot.render);
                animator.SetOverlay("right", item.RightAnim, slot.render);
                slot.render.color = item.Color;
                slot.render.sortingOrder = item.OrderInLayer;
            }
            else
            {
                animator.RemoveOverlay("up", slot.render);
                animator.RemoveOverlay("down", slot.render);
                animator.RemoveOverlay("left", slot.render);
                animator.RemoveOverlay("right", slot.render);
                slot.render.color = Color.white;
            }
        }

        private void UpdateView(int slot, Item item)
        {
            if (slot < 0 || slot >= Mathf.Min(slots.Count, underwear.Length))
            {
                return;
            }

            var clothe = item? item : underwear[slot];

            AssignAnimations(clothe, slots[slot]);
        }


        public void SwapItems(int slot)
        {
            var inv = PlayerInventory.Instance;
            
            if (slot < 0 || slot >= items.Length)
                return;

            if (inv.InHand)
            {
                var cat = inv.InHand.Category;
                var _slot = slots[slot];
                if (_slot.category != cat)
                    return;

            }
            
            inv.SwapItem(this, slot);
            UpdateView(slot, items[slot]);
        }
    }
}
