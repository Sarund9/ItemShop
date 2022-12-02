using AYellowpaper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class PlayerEquipClicker : MonoBehaviour
    {

        [SerializeField]
        Vector2 offset;

        [SerializeField]
        PlayerClothes clothes;

        [SerializeField]
        RectTransform self;

        [SerializeField]
        RectTransform[] children;

        [SerializeField]
        float[] percents;

        Camera cam;
        Transform player;

        Vector2Int lastScreenSize;

        private void Start()
        {
            cam = GM.Camera;
            player = PlayerController.Instance.transform;

            RefreshSizes();
        }

        private void Update()
        {
            transform.position = player.position + (Vector3)offset;

            RefreshSizes();
        }

        private void RefreshSizes()
        {
            lastScreenSize = new Vector2Int(Screen.width, Screen.height);
            
            for (int i = 0; i < Mathf.Min(children.Length, percents.Length); i++)
            {
                var child = children[i];
                var p = percents[i];
                var totalHeight = self.sizeDelta.y;

                var s = child.sizeDelta;
                s.y = totalHeight * p;

                child.sizeDelta = s;
            }
        }

        // Called by Buttons
        public void Clicked(int slot)
        {
            clothes.SwapItems(slot);
        }
    }
}
