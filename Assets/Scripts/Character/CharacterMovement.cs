using UnityEngine;

namespace Assets.Scripts
{
    public abstract class CharacterMovement : MonoBehaviour
    {
        public abstract Rigidbody2D CharacterRigidBody { get; }
    }
}