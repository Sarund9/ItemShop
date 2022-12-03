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

        public bool MovementEnabled => !PlayerInventory.Instance.Open;

        public void OnInteract(InputAction.CallbackContext context)
        {
            
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (MovementEnabled)
                rb.velocity = context.ReadValue<Vector2>() * moveSpeed;
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            GM.Input.SubscribePlayer(this);
            var mod = GM.CameraControl.CreateModifier();
            mod.Target = transform;

        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var inv = PlayerInventory.Instance;
                inv.SwapOpen();
                if (!MovementEnabled)
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }
}
