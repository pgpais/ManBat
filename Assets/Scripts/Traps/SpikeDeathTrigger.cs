using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class SpikeDeathTrigger : MonoBehaviour
    {
        public SpikeTrap Spiketrap;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Player>() != null)
                Spiketrap.ActivateSpikes();
        }
    }
}
