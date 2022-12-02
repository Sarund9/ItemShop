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

        public Item this[int slot]
        {
            get => selection[slot];
            //set {
            //    // Do not allow overriding of Slot Items
            //    if (selection[slot] != null)
            //        return;

            //    inventory[slot] = value;
            //}
        }

        public bool TrySet(int slot, Item item)
        {
            // Do not allow overriding of Slot Items
            if (selection[slot] != null)
                return false;

            inventory[slot] = item;
            return true;
        }


        [field: SerializeField, Range(1, 20)]
        public int Width { get; private set; }

        [field: SerializeField, Range(1, 20)]
        public int Height { get; private set; }

        public event Action<int> OnItemChanged;


        public void OpenShop()
        {
            for (int i = 0; i < selection.Count; i++)
            {
                inventory[i] = selection[i];
            }
        }

        public void Awake()
        {
            inventory = new Item[selection.Count];
        }

    }
}
