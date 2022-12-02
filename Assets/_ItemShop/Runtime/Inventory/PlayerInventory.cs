using AYellowpaper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ItemShop
{
    public class PlayerInventory : MonoBehaviour, IContainer
    {
        public static PlayerInventory Instance { get; private set; }

        [SerializeField]
        Item[] items;

        [SerializeField]
        CursorSprite cursor;

        [SerializeField]
        GameObject ui;

        [SerializeField]
        DialoguePlayer player;

        [field: SerializeField, Range(1, 20)]
        public int Width { get; private set; }

        [field: SerializeField, Range(1, 20)]
        public int Height { get; private set; }

        public Item this[int slot]
        {
            get => items[slot];
        }

        public bool TrySet(int slot, Item item)
        {
            items[slot] = item;
            return true;
        }

        public Item InHand { get; private set; }

        public bool Open { get; private set; }

        [field: SerializeField]
        public int Money { get; private set; }

        public event Action<int> OnItemChanged;

        public event Action<int> OnMoneyChanged;

        public int IndexOf(int x, int y) =>
            x + y * Width;

        private void Awake()
        {
            OnValidate();
            Instance = this;
        }

        public void Start()
        {
            if (OnItemChanged == null)
                return;
            for (int i = 0; i < items.Length; i++)
            {
                OnItemChanged.Invoke(i);
            }
        }

        // Attempt to move item from Shop Container to Hand
        public bool TryBuy(IContainer buyfrom, int buyslot)
        {
            if (InHand)
                return false;

            if (buyslot < 0 || buyslot >= buyfrom.Width * buyfrom.Height)
                return false;

            var item = buyfrom[buyslot];

            if (Money < item.Price)
                return false;

            Money -= item.Price;
            SwapItem(buyfrom, buyslot);

            return true;
        }

        // Attempt to sell item by placing in Shop Container
        public bool TrySell(IContainer sellto, int sellslot)
        {
            if (!InHand)
                return false;

            if (sellslot < 0 || sellslot >= sellto.Width * sellto.Height)
                return false;

            var item = sellto[sellslot];

            Money += item.Price;
            SwapItem(sellto, sellslot);

            return true;
        }


        public void SwapOpen()
        {
            Open = !Open;
            ui.SetActive(Open);
            if (Open)
            {
                // Opened
                if (player.DisplayActive)
                {
                    player.Close();
                }
            }
            else
            {
                // Closed
                if (InHand)
                {
                    // Find the first empty slot and drop the Item
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i] == null)
                        { SwapItem(this, i); break; }
                    }
                    
                }
            }
        }

        private void OnValidate()
        {
            var old = items;
            items = new Item[Width * Height];

            Array.Copy(old, items, Mathf.Min(old.Length, items.Length));
        }

        public Item GetItem(int x, int y)
        {
            return items[IndexOf(x, y)];
        }

        public bool SwapItem(IContainer container, int slot)
        {
            var con = container[slot];
            if (!container.TrySet(slot, InHand))
                return false;
            InHand = con;


            OnItemChanged?.Invoke(slot);

            cursor.Set(InHand);

            // TODO: Only do this when Mouse is used
            EventSystem.current.SetSelectedGameObject(null);
            return true;
        }
    }

    public interface IContainer
    {
        public int Width { get; }
        public int Height { get; }
        
        public Item this[int slot] { get; }

        public event Action<int> OnItemChanged;

        public bool TrySet(int slot, Item item) => false;

        public int IndexOf(int x, int y) =>
            x + y * Width;

    }
}
