using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ItemShop
{
    public class DialoguePlayer : MonoBehaviour
    {
        public static DialoguePlayer Instance { get; private set; }

        [SerializeField]
        TMP_Text text;

        public static bool Exists(out DialoguePlayer player)
        {
            if (Instance) {
                player = Instance;
                return true;
            }
            player = null;
            return false;
        }


        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        public void DisplayLine(string line)
        {
            gameObject.SetActive(true);
            text.text = line;
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
