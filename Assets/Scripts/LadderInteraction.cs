using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.transform.position = transform.position + Vector3.up ;
		}
	}
}
