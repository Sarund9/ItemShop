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

        void Start()
        {
            GM.Input.SubscribePause(this);
        }

        // Called by Resume Button
        public void TogglePause()
        {
            gamePaused = !gamePaused;
            pauseMenu.SetActive(gamePaused);
            print($"Set game pause {gamePaused}");
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
