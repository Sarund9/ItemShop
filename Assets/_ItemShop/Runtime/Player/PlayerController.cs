using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ItemShop
{
    public class PlayerController : MonoBehaviour, PlayerInput.ICharacterActions
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField, Range(1, 20f)]
        float moveSpeed = 5f;

        /*
        I went for a simple Character Control system
        If it was more complex the logic would be in it's own class

         */
        [SerializeField]
        Rigidbody2D rb;

        [SerializeField]
        PlayerInventory inventory;

        public void OnInteract(InputAction.CallbackContext context)
        {
            
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            rb.velocity = context.ReadValue<Vector2>() * moveSpeed;
        }

        void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            GM.Input.SubscribePlayer(this);
        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                inventory.SwapOpen();
            }
        }
    }
}
