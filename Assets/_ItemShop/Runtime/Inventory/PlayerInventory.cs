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

        public event Action<int> OnItemChanged;

        [field: SerializeField, Range(1, 20)]
        public int Width { get; private set; }

        [field: SerializeField, Range(1, 20)]
        public int Height { get; private set; }

        [SerializeField]
        CursorSprite cursor;

        [SerializeField]
        GameObject ui;

        [SerializeField]
        DialoguePlayer player;

        public Item this[int slot]
        {
            get => items[slot];
            set => items[slot] = value;
        }

        public Item InHand { get; private set; }

        public bool Open { get; private set; }

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

        public void OnGUI()
        {
            GUILayout.Label($"DEBUG\n" +
                $"In Hand: {InHand}");
        }

        public Item GetItem(int x, int y)
        {
            return items[IndexOf(x, y)];
        }

        public void SwapItem(IContainer container, int slot)
        {
            var hand = InHand;
            InHand = container[slot];
            container[slot] = hand;

            OnItemChanged?.Invoke(slot);

            cursor.Set(InHand);

            // TODO: Only do this when Mouse is used
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public interface IContainer
    {
        public int Width { get; }
        public int Height { get; }
        
        public Item this[int slot] { get; set; }

        public event Action<int> OnItemChanged;

        public int IndexOf(int x, int y) =>
            x + y * Width;

    }
}
