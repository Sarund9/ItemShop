using System;
using System.Collections;
using System.Collections.Generic;
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

        Vector2 offset;



        private void Update()
        {
            var mousePos = Mouse.current.position.ReadValue();

            image.transform.position = mousePos + (offset * offsetMult);

            //var wp = GM.Camera.ScreenToWorldPoint(mousePos);

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
