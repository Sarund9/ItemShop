using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

        bool gamePaused = false;

        public bool MovementEnabled => !PlayerInventory.Instance.Open;

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || Selectable.allSelectableCount == 0)
                return;

            //var toselect = Selectable.allSelectablesArray[0];
            //EventSystem.current.SetSelectedGameObject(toselect.gameObject);
            //print($"SELECT: {toselect.gameObject.name}");
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (MovementEnabled && !gamePaused)
                rb.velocity = context.ReadValue<Vector2>() * moveSpeed;
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            var mod = GM.CameraControl.CreateModifier();
            mod.Target = transform;
            GM.OnGamePaused += p => gamePaused = p;
        }

        private void OnEnable()
        {
            GM.Input.SubscribePlayer(this);
        }
        private void OnDisable()
        {
            GM.Input.UnsubscribePlayer();
        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            if (context.performed && !gamePaused)
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
