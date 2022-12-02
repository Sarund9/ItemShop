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

        Camera cam;
        Transform player;

        private void Start()
        {
            cam = GM.Camera;
            player = PlayerController.Instance.transform;
        }


        private void Update()
        {
            //var pos = cam.WorldToScreenPoint(player.position);

            transform.position = player.position + (Vector3)offset;
        }

        public void Clicked(int slot)
        {
            clothes.SwapItems(slot);
        }
    }
}
