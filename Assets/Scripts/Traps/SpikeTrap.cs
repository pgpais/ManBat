using Assets.Scripts.Lighting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class SpikeTrap : MonoBehaviour
    {
        public GameObject SpikesGameObject;
        public Transform SpikeTrapContainer;
        public Transform SoundSpawnLocation;
        public float WarningInterval;
        public AudioSource AudioSource;
        public AudioClip SpikesArmAudioClip;
        public AudioClip SpikesShootClip;


        [Range(0,2)]
        public float CloseSoundIntensity;
        [Range(0, 2)]
        public float LoadingSoundIntensity;
        [Range(0, 2)]
        public float ActivateSoundIntensity;


        public float TimeToActivate;
        public float TimeToDeactivate;

        private bool _activated = false;

        public void ActivateSpikes()
        {
            if (!_activated)
                StartCoroutine(ActivateSpikesRoutine());

        }

        public void DeactivateSpikes()
        {
            SpikesGameObject.SetActive(false);
            _activated = false;
        }

        public LightMask PerformCloseSound()
        {
            var light = LightManager.Instance.CreateLight(SoundSpawnLocation.position, CloseSoundIntensity);
            return light;

        }


        public LightMask PerformActivationSound()
        {
            var light = LightManager.Instance.CreateLight(SoundSpawnLocation.position, ActivateSoundIntensity);
            
            return light;
        }


        public IEnumerator ActivateSpikesRoutine()
        {
            _activated = true;
            AudioSource.clip = SpikesArmAudioClip;
            AudioSource.loop = true;
            AudioSource.Play();
            for (int i = 0; i < Mathf.CeilToInt(TimeToActivate / WarningInterval); i++)
            {
                LightManager.Instance.CreateLight(SoundSpawnLocation.position, LoadingSoundIntensity);
                yield return new WaitForSeconds(WarningInterval);
            }

            AudioSource.Pause();
            AudioSource.clip = SpikesShootClip;
            AudioSource.loop = false;
            AudioSource.Play();

            PerformActivationSound();
            yield return new WaitForSeconds(0.3f);

            SpikesGameObject.SetActive(true);
            yield return new WaitForSeconds(TimeToDeactivate);
            DeactivateSpikes();
        }
    }
}
