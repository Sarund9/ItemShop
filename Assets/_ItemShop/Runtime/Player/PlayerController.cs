using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ItemShop
{
    public class PlayerController : MonoBehaviour, PlayerInput.ICharacterActions
    {


        [SerializeField, Range(.1f, 10f)]
        float moveSpeed = 2f;

        Vector2 moveInput;
        
        public void OnInteract(InputAction.CallbackContext context)
        {
            
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void Start()
        {
            GM.Input.SubscribePlayer(this);
        }

        public void Update()
        {
            transform.position += moveSpeed * Time.deltaTime * new Vector3(moveInput.x, moveInput.y, 0);


        }


    }
}
