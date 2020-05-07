using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyCharacter: MonoBehaviour
    {
		public PlayerInteraction PlayerInteraction;
		public EnemyMovement EnemyMovement;
        public SoundEmission EnemySoundEmission;

		public void Reset(){
			EnemyMovement.Reset ();
		}
    }
}