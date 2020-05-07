using Assets.Scripts.Lighting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour {

    public GameObject arrow;
    //public AudioSource arrowFiredSound;
    public GameObject temp;
    public float arrowSpeed;
    public float timeBetweenArrows;
    public Transform arrowLauncher;
    public AudioSource source;
	private bool playSound = true;

    [Header("Light Settings")]

    public float lightRadius;
    public float lightIntensity;
    public float lightLifetime;

    private float launchTime = 0;

	// Use this for initialization
	void Start () {
        arrowLauncher = gameObject.transform.GetChild(0);
        LaunchArrow();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > launchTime)
        {
            LaunchArrow();
        }
	}
	void OnBecameInvisible(){
		playSound = false;
	}

	void OnBecameVisible(){
		playSound = true;
	}
    void LaunchArrow()
    {
        var light = LightManager.Instance.CreateLight(gameObject.transform.position, 1f);
		if (playSound) {
			source.Play();
		}
        temp = Instantiate<GameObject>(arrow, arrowLauncher.position, gameObject.transform.rotation);
        launchTime += timeBetweenArrows;
        temp.GetComponent<Arrow>().Velocity = arrowSpeed * -temp.transform.right;
        
    }
}
