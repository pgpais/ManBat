using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraTracking : MonoBehaviour
    {
		[Tooltip("Player GameObject: Drag here")]
        public Transform Player;
		[Tooltip("Screen height in squares")]
		public float height;
		[Tooltip("The speed of the transition")]
		public float transitionSpeed;

		Vector3 newPosition;
		Vector3 startPosition;
		void Start(){
			if (Player == null) {
				Player = GameObject.Find ("Player").transform;
			}
				
			newPosition = transform.position;
		}

        void Update() {
			float width = height * Screen.width/Screen.height;
			if (Time.timeScale > 0) {
				if (Player.transform.position.x >= transform.position.x + width / 2) {
					newPosition = transform.position + Vector3.right * width;
				}
				if (Player.transform.position.x <= transform.position.x - width / 2) {
					newPosition = transform.position + Vector3.left * width;
				}
				if (Player.transform.position.y >= transform.position.y + height / 2) {
					newPosition = transform.position + Vector3.up * height;
				}
				if (Player.transform.position.y <= transform.position.y - height / 2) {
					newPosition = transform.position + Vector3.down * height;
				}
				startPosition = transform.position;
			}
			if (transform.position != newPosition) {
				Transition (newPosition);
			}
        }

		void Transition(Vector3 newPosition){
			Time.timeScale = 0.0f;
			float step = transitionSpeed * Vector3.Distance(startPosition, newPosition) ;
			transform.position = Vector3.MoveTowards (transform.position, newPosition, step);
			if (transform.position == newPosition) {
				Time.timeScale = 1.0f;
			}
		}

    }
}
