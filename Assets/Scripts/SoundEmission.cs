using Assets.Scripts.Lighting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent (typeof(AudioSource))]
    public class SoundEmission : MonoBehaviour
    {
        public CharacterMovement CharacterMovement;
        
        public Transform LightSpawnPosition;
        public float FallSoundMultiplied = 0.5f;
        public AudioClip[] audios;
        public AudioSource source;

        private bool _falling;
        private float _fallVelocity;
		private bool playSound = true;

        public float SoundFactor = 0.7f;

        void OnDisable ()
        {
            _falling = false;
            _fallVelocity = 0;
        }

        void Update()
        {
            if (CharacterMovement.CharacterRigidBody.velocity.y < 0)
            {
                _falling = true;
                _fallVelocity = Mathf.Min(CharacterMovement.CharacterRigidBody.velocity.y, _fallVelocity);
            }
            else
            {
                if (_falling && CharacterMovement.CharacterRigidBody.velocity.y == 0)
                {
                    EmitSoundWithSpeed(Math.Abs(_fallVelocity) * FallSoundMultiplied);
                    _falling = false;
                    _fallVelocity = 0;
                }
            }
        }

        public void EmitSound()
        {
            var fallVelocity = CharacterMovement.CharacterRigidBody.velocity;
            if (_falling)
            {
                fallVelocity.x = 0;
            } else
            {
                fallVelocity.y = 0;
            }
            int i = UnityEngine.Random.Range(0, audios.Length);
			if (playSound) {
				source.clip = audios[i];
				source.Play();
			}
            EmitSoundWithSpeed(fallVelocity.magnitude);
        }
		void OnBecameInvisible(){
			playSound = false;
		}

		void OnBecameVisible(){
			playSound = true;
		}

        private void EmitSoundWithSpeed(float speed)
        {
            speed = Mathf.Clamp(speed, 0.5f, 2f);
            speed *= SoundFactor;
            var light = LightManager.Instance.CreateLight(LightSpawnPosition.transform.position, speed);
        }
    }
}
