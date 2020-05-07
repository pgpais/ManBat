using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public CharacterControl CharacterMovement;

        public void Reset()
        {
            CharacterMovement.Respawn();
        }
    }
}
