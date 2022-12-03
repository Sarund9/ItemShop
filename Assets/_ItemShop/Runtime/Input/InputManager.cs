using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class InputManager : MonoBehaviour
    {
        PlayerInput input;


        //public bool PlayerEnabled
        //{
        //    get => input.Character.enabled;
        //    set
        //    {
        //        if (value)
        //            input.Character.Enable();
        //        else
        //            input.Character.Disable();
        //    }
        //}

        private void Awake()
        {
            input = new();
        }

        public void SubscribePlayer(PlayerInput.ICharacterActions character)
        {
            input.Character.Enable();
            input.Character.SetCallbacks(character);
        }

        public void SubscribePause(PlayerInput.IPauseActions pause)
        {
            input.Pause.Enable();
            input.Pause.SetCallbacks(pause);
        }

        public void UnsubscribePlayer()
        {
            input.Character.SetCallbacks(null);
            input.Character.Disable();
        }

        public void UnsubscribePause()
        {
            input.Pause.SetCallbacks(null);
            input.Pause.Disable();
        }
    }
}
