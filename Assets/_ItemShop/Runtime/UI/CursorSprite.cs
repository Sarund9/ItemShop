using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ItemShop
{
    public class CursorSprite : MonoBehaviour
    {


        [SerializeField]
        Image image;

        [SerializeField]
        float offsetMult = 3;

        [SerializeField]
        TMP_Text tooltipText;

        [SerializeField]
        GameObject tooltip;

        Vector2 offset;

        private void Update()
        {
            // Main Item
            var mousePos = Mouse.current.position.ReadValue();

            transform.position = mousePos + (offset * offsetMult);

            var slot = TooltipTarget.Current;
            tooltip.SetActive(slot && slot.Item);

            if (slot && slot.Item)
            {
                tooltipText.text = $"{slot.Item.ItemName}\n" +
                    $"${slot.Item.Price}\n" +
                    slot.Item.Category.CategoryName + "\n" + 
                    slot.Item.Description;
            }
        }

        public void Set(Item inHand)
        {
            if (inHand)
            {
                image.sprite = inHand.Sprite;
                image.color = inHand.Color;
                image.transform.localScale = Vector3.one * inHand.SpriteScale;
                offset = inHand.SpriteOffset;
                Cursor.visible = false;
            }
            else
            {
                image.color = Color.white * 0;
                image.transform.localScale = Vector3.one;
                offset = Vector2.zero;
                Cursor.visible = true;
            }
        }
    }
}
