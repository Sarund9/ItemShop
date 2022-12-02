using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ItemShop
{
    public class TextButton : MonoBehaviour
    {

        [SerializeField]
        TMP_Text text;

        [SerializeField]
        Button button;

        public string Text
        {
            get => text.text;
            set => text.text = value;
        }

        public void SetCallback(UnityAction callback)
        {
            button.onClick.AddListener(callback);
        }

        public void Delete()
        {
            Destroy(gameObject);
        }
    }
}
