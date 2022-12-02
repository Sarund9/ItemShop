using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ItemShop
{
    public class DialoguePlayer : MonoBehaviour
    {
        public static DialoguePlayer Instance { get; private set; }
        public bool DisplayActive { get; private set; }

        [SerializeField]
        TMP_Text text;

        [SerializeField]
        TextButton optionPrefab;

        [SerializeField]
        Transform optionParent;

        List<TextButton> options = new();

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

        public void ClearOptions()
        {
            foreach (var item in options)
            {
                item.Delete();
            }
            options.Clear();
        }

        public void DisplayLine(string line)
        {
            var inv = PlayerInventory.Instance;
            if (inv && inv.Open)
                inv.SwapOpen();

            DisplayActive = true;

            gameObject.SetActive(true);
            text.text = line;
        }

        public void CreateOption(string name, UnityAction callback)
        {
            var btn = Instantiate(optionPrefab, optionParent);

            btn.Text = name;
            btn.SetCallback(callback);

            options.Add(btn);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            DisplayActive = false;
        }
    }
}
