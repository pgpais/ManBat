using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class SpikeTrapSoundTrigger: MonoBehaviour
    {
        public SpikeTrap spikeTrap;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Player>() != null)
                spikeTrap.ActivateSpikes();
        }
    }
}
