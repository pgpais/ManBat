using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public CharacterControl CharacterMovement;

        public void Reset()
        {
            CharacterMovement.Respawn();
        }
        
        

        #region Input callbacks

        public void OnMovement(InputAction.CallbackContext ctx)
        {
            float mov = ctx.ReadValue<float>();

            CharacterMovement.ReceiveMovInput(mov);
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            Debug.Log(ctx.phase);
            if (ctx.performed)
            {
                CharacterMovement.ReceiveJumpInput(true);
            }

            if (ctx.canceled)
            {
                CharacterMovement.ReceiveJumpInput(false);
            }
        }

        #endregion
    }
}
