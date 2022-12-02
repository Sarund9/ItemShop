using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemShop
{
    public partial class GM : MonoBehaviour
    {
        static GM I;

        [SerializeField]
        SceneRef menu;

        [Header("Managers")]
        [SerializeField]
        InputManager input;
        [SerializeField]
        Camera cam;

        public static InputManager Input => I.input;

        public static Camera Camera => I.cam;

        private void Awake()
        {
            if (I)
            {
                Debug.LogError("Duplicate GM!, Abort!");
                Debug.Break();
                return;
            }
            I = this;

            // In the Game Build, load the Menu Scene
#if !UNITY_EDITOR
            menu.LoadAsync();
#endif
        }

        public static Coroutine RunGlobalCoroutine(IEnumerator cor)
        {
            return I.StartCoroutine(cor);
        }


    }

}

