﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Lighting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public CharacterControl CharacterMovement;
        public GameObject inputUi;
        public float screechSpeed = 5f;
        public Transform head;

        private void Start()
        {
#if UNITY_ANDROID || UNITY_IOS
            Debug.Log("Android or IOS");
            inputUi.SetActive(true);
            
#else
            inputUi.SetActive(false);
#endif
            
        }

        public void Reset()
        {
            CharacterMovement.Respawn();
        }
        
        

        #region Input callbacks

        public void OnMovement(InputAction.CallbackContext ctx)
        {
            float mov = ctx.ReadValue<Vector2>().x;

            CharacterMovement.ReceiveMovInput(mov);
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                CharacterMovement.ReceiveJumpInput(true);
            }

            if (ctx.canceled)
            {
                CharacterMovement.ReceiveJumpInput(false);
            }
        }
        
        public void OnRun(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                CharacterMovement.ReceiveRunInput(true);
            }

            if (ctx.canceled)
            {
                CharacterMovement.ReceiveRunInput(false);
            }
        }

        public void OnMakeSound(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                LightManager.Instance.CreateLight(head.position, screechSpeed);
            }
        }

        public void OnTouch(InputAction.CallbackContext ctx)
        {
            Debug.Log(ctx.ReadValue<Vector2>());
        }

        #endregion
    }
}
