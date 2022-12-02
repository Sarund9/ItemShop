using AYellowpaper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ItemShop
{
    public class ContainerUI : MonoBehaviour
    {
        
        // Container that this UI Displays and Interfaces with
        [SerializeField]
        InterfaceReference<IContainer> container;

        [SerializeField]
        ContainerUIItem itemPrefab;

        [SerializeField]
        Transform layout;

        ContainerUIItem[] slots;

        [Header("Images")]
        [SerializeField]
        int slotSize = 64;
        [SerializeField]
        RectTransform self;

        [SerializeField]
        Sprite
            topLeft, top, topRight,
            left, center, right,
            downLeft, down, downRight;

        [SerializeField]
        Sprite
            topLeft_pressed, top_pressed, topRight_pressed,
            left_pressed, center_pressed, right_pressed,
            downLeft_pressed, down_pressed, downRight_pressed;

        //public PlayerInventory Inventory => inventory;
        public IContainer TargetContainer
        {
            get => container.Value;
            set => container.Value = value;
        }

        private void OnButtonPressed(int slot)
        {
            var inv = PlayerInventory.Instance;

            inv.SwapItem(container.Value, slot);

            //if (inv.InHand != null)
            //{
            //    var swap = inventory[slot];
            //    inventory[slot] = inv.InHand;
            //    inv.InHand = swap;
            //}
        }

        private void Awake()
        {
            if (container.Value == null)
                return;

            var inventory = container.Value;
            inventory.OnItemChanged += Inventory_OnItemChanged;

            self.sizeDelta = new Vector2(slotSize * inventory.Width, slotSize * inventory.Height);

            slots = new ContainerUIItem[inventory.Width * inventory.Height];
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = Instantiate(itemPrefab, layout);

                int W = inventory.Width,
                    H = inventory.Height;

                bool istop = i < W;
                bool isbottom = i >= W * (H - 1);
                bool isleft = i % W == 0;
                bool isright = i % W == (H - 1);

                Sprite
                    sprite = center,
                    pressed = center_pressed;

                #region Determine
                if (isleft)
                {
                    if (istop)
                    {
                        sprite = topLeft;
                        pressed = topLeft_pressed;
                    }
                    else if (isbottom)
                    {
                        sprite = downLeft;
                        pressed = downLeft_pressed;
                    }
                    else
                    {
                        sprite = left;
                        pressed = left_pressed;
                    }
                }
                else if (isright)
                {
                    if (istop)
                    {
                        sprite = topRight;
                        pressed = topRight_pressed;
                    }
                    else if (isbottom)
                    {
                        sprite = downRight;
                        pressed = downRight_pressed;
                    }
                    else
                    {
                        sprite = right;
                        pressed = right_pressed;
                    }
                }
                else
                {
                    if (istop)
                    {
                        sprite = top;
                        pressed = top_pressed;
                    }
                    else if (isbottom)
                    {
                        sprite = down;
                        pressed = down_pressed;
                    }
                }
                #endregion

                slots[i].SetCallback(OnButtonPressed);
                slots[i].Initialize(sprite, pressed, i);
            }

            gameObject.SetActive(false);
        }

        private void Inventory_OnItemChanged(int index)
        {
            slots[index].SetItem(container.Value[index]);
        }
    }
}
