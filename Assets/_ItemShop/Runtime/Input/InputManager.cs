using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class InputManager : MonoBehaviour
    {
        PlayerInput input;


        private void Awake()
        {
            input = new();
        }

        public void SubscribePlayer(PlayerInput.ICharacterActions character)
        {
            input.Character.Enable();
            input.Character.SetCallbacks(character);
        }

    }
}
