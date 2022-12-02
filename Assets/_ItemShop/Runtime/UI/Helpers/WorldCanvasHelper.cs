using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class WorldCanvasHelper : MonoBehaviour
    {

        [SerializeField]
        Canvas canvas;


        private void Start()
        {
            if (canvas && canvas.renderMode == RenderMode.WorldSpace)
            {
                canvas.worldCamera = GM.Camera;
            }
        }
    }
}
