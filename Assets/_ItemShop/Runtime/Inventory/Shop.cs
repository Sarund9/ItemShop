using System;
using System.Collections;
using UnityEngine;

namespace ItemShop
{
    public class Shop : MonoBehaviour, IContainer
    {

        [SerializeField]
        ShopSelection selection;

        Item[] inventory;

        [field: SerializeField, Range(1, 20)]
        public int Width { get; private set; }

        [field: SerializeField, Range(1, 20)]
        public int Height { get; private set; }

        public event Action<int> OnItemChanged;

        public Item this[int slot] => inventory[slot];

        public bool IsShop => true;

        public bool TrySet(int slot, Item item)
        {
            // Do not allow overriding of Slot Items
            if (slot < selection.Count && selection[slot] != null)
                return false;

            inventory[slot] = item;
            OnItemChanged?.Invoke(slot);
            return true;
        }

        private void FillShop()
        {
            for (int i = 0; i < selection.Count; i++)
            {
                inventory[i] = selection[i];
                OnItemChanged?.Invoke(i);
            }
        }

        public void Start()
        {
            inventory = new Item[Width * Height];

            FillShop();
        }

        public void ShopOpened()
        {
            for (int i = 0; i < selection.Count; i++)
            {
                OnItemChanged?.Invoke(i);
            }
        }

    }
}
