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

        [SerializeField]
        Rigidbody2D rb;


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


    }
}
