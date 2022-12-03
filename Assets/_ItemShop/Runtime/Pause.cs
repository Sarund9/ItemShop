using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class Pause : MonoBehaviour, PlayerInput.IPauseActions
    {
        
        [SerializeField]
        GameObject pauseMenu;

        [SerializeField]
        SceneRef world, menu;

        bool gamePaused = false;

        public bool GamePaused => gamePaused;

        public event Action<bool> OnGamePaused;

        void OnEnable()
        {
            GM.Input.SubscribePause(this);
        }
        void OnDisable()
        {
            GM.Input.UnsubscribePause();
        }

        // Called by Resume Button
        public void TogglePause()
        {
            var inv = PlayerInventory.Instance;
            if (inv.Open)
                inv.SwapOpen();
            
            gamePaused = !gamePaused;
            pauseMenu.SetActive(gamePaused);
            OnGamePaused?.Invoke(gamePaused);
        }

        public void OnToggle(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.performed && world.IsLoaded)
            {
                TogglePause();
            }
        }

        // Called by Back Button
        public void ReturnToMainMenu()
        {
            TogglePause();
            world.UnloadAsync();
            menu.LoadAsync();
        }
    }
}
