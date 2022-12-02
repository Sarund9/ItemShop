using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class PlayerEquipment : MonoBehaviour, IContainer
    {

        [SerializeField]
        Vector2 offset;

        [SerializeField]
        Item[] items = new Item[1];

        Camera cam;
        Transform player;

        public event Action<int> OnItemChanged;

        public int Width => items.Length;

        public int Height => 1;

        public Item this[int slot] {
            get => items[slot];
            set {
                items[slot] = value;
                OnItemChanged?.Invoke(slot);
            }
        }

        private void Start()
        {
            cam = GM.Camera;
            player = PlayerInventory.Instance.transform;
            //OnValidate();
        }

        //private void OnValidate()
        //{
        //    items = new Item[slotCount];
        //}

        private void Update()
        {
            var pos = cam.WorldToScreenPoint(player.position);

            transform.position = pos + (Vector3)offset;
        }

        public void Clicked(int slot)
        {
            var d = PlayerInventory.Instance;
            
            // TODO: Check that the Item can 

            d.ItemSwapped(this, slot);
        }
    }
}
