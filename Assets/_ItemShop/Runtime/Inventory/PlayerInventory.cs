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
        DialoguePlayer dialoguePlayer;

        [SerializeField]
        ContainerUI shopUi;

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

        public Shop CurrentShop { get; private set; }

        [field: SerializeField]
        public int Money { get; private set; }

        public event Action<int> OnItemChanged;

        public event Action<int> OnMoneyChanged;

        CameraController.Modifier whenOpen;

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

        public void DoTransaction(IContainer con, int slot)
        {
            if (InHand)
            {
                // Sell: Attempt by placing item in Shop Container from Hand
                var prev = con[slot];
                // If exchanging item, check that difference is valid
                if (prev && prev.Price - InHand.Price < Money)
                    return;
                
                // Shops will not allow selling at a 'selection' slot
                if (!con.TrySet(slot, InHand))
                    return;
                InHand = prev;

                // Get money from item sold
                Money += con[slot].Price;
                // Substract money if item was also bought
                if (InHand)
                    Money -= InHand.Price;

                OnMoneyChanged?.Invoke(Money);
            }
            else
            {
                // Buy: Attempt to move item from Shop Container to Hand
                var tobuy = con[slot];
                
                // No item there
                if (!tobuy)
                    return;

                // Cannot buy item
                if (Money < tobuy.Price)
                    return;

                InHand = tobuy;
                // Do not care if item will be duplicated
                con.TrySet(slot, null);

                Money -= InHand.Price;
                OnMoneyChanged?.Invoke(Money);
            }
            cursor.Set(InHand);
        }

        public void OpenShop(Shop shop)
        {
            if (!Open)
                SwapOpen();

            shopUi.TargetContainer = shop;
            shopUi.gameObject.SetActive(true);
            CurrentShop = shop;
            shop.ShopOpened();
        }

        public void SwapOpen()
        {
            Open = !Open;
            ui.SetActive(Open);
            if (Open)
            {
                // Opened
                if (dialoguePlayer.DisplayActive)
                {
                    dialoguePlayer.Close();
                }

                whenOpen = GM.CameraControl.CreateModifier();
                whenOpen.Zoom = 2f;
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
                if (CurrentShop)
                {
                    shopUi.TargetContainer = null;
                    shopUi.gameObject.SetActive(false);
                    CurrentShop = null;
                }
                whenOpen.Disable();
                whenOpen = null;
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

        public bool IsShop => false;

        public bool TrySet(int slot, Item item) => false;

        public int IndexOf(int x, int y) =>
            x + y * Width;
    }

}
