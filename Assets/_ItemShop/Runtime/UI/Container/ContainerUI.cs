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
            set {
                container.Value = value;
                if (value != null && Application.isPlaying)
                    CreateUI();
            }
        }

        private void OnButtonPressed(int slot)
        {
            var inv = PlayerInventory.Instance;

            inv.SwapItem(container.Value, slot);
        }

        private void Awake()
        {
            if (container.Value == null)
                return;
            
            CreateUI();
        }

        private void CreateUI()
        {
            var inv = container.Value;
            inv.OnItemChanged += ItemChanged;

            self.sizeDelta = new Vector2(slotSize * inv.Width, slotSize * inv.Height);

            // Prevent extra slots on Recall
            if (slots != null)
                foreach (var item in slots)
                    if (item)
                        Destroy(item.gameObject);
            

            slots = new ContainerUIItem[inv.Width * inv.Height];
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = Instantiate(itemPrefab, layout);

                int W = inv.Width,
                    H = inv.Height;

                bool istop = i < W;
                bool isbottom = i >= W * (H - 1);
                bool isleft = i % W == 0;
                bool isright = i % W == (H - 1);

                Sprite
                    sprite = center,
                    pressed = center_pressed;

                #region Determine Sprite
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

        private void ItemChanged(int index)
        {
            slots[index].SetItem(container.Value[index]);
        }
    }
}
