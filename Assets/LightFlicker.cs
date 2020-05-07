using Assets.Scripts.Lighting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public float LightPower = 1;
    public float FlickerCooldown;

    private float flickerTime;
    private LightMask lightMask;

	// Use this for initialization
	void Start () {
        lightMask = LightManager.Instance.CreateLight(transform.position, LightPower);
        flickerTime = Time.time + FlickerCooldown;
    }
	
	// Update is called once per frame
	void Update () {
        float velocity = 0;
        float radius = lightMask.Radius;
        if (Time.time > flickerTime)
        {
            /*if(radius >= maxRadius)
                radius = Mathf.SmoothDamp(radius, 0, ref velocity, FlickerCooldown);
            else if(radius <= 0)
                radius = Mathf.SmoothDamp(radius, maxRadius, ref velocity, FlickerCooldown);*/
            lightMask = LightManager.Instance.CreateLight(transform.position, LightPower);
            flickerTime = Time.time + FlickerCooldown;
        }
	}
}
