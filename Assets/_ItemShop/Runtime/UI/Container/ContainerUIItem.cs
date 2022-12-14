using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ItemShop
{
    public class ContainerUIItem : MonoBehaviour, IItemDisplay
    {
        [SerializeField]
        Item item;

        [SerializeField]
        Image itemDisplay;

        [SerializeField]
        Image background;

        [SerializeField]
        Button button;

        [SerializeField]
        RectTransform toScale, toOffset;

        int index;

        public Item Item
        {
            get => item;
            set
            {
                item = value;
                OnValidate();
            }
        }

        private void OnValidate()
        {
            if (itemDisplay && toOffset && toScale)
                SetItem(item);
        }

        public void SetCallback(Action<int> callback)
        {
            button.onClick.AddListener(() => callback(index));
        }

        public void Initialize(Sprite image, Sprite onPressed, int index)
        {
            background.sprite = image;
            var s = button.spriteState;
            s.highlightedSprite = onPressed;
            s.selectedSprite = onPressed;
            s.pressedSprite = onPressed;
            button.spriteState = s;
            this.index = index;
        }

        private void SetItem(Item item)
        {
            if (item)
            {
                itemDisplay.sprite = item.Sprite;
                itemDisplay.color = item.Color;
                toOffset.localPosition = item.SpriteOffset;
                toScale.localScale = item.SpriteScale * Vector3.one;
            }
            else
            {
                itemDisplay.sprite = null;
                itemDisplay.color = Color.white * 0;
                toOffset.localPosition = Vector3.zero;
                toScale.localScale = Vector3.one;
            }
            
        }

    }
}
