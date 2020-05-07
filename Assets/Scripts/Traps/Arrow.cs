using Assets.Scripts;
using Assets.Scripts.Lighting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float FlickerTime;
    public Vector2 Velocity;
    public float LightPower;

    private float nextFlicker;
    // Use this for initialization
    void Start()
    {
        nextFlicker = Time.time;
        gameObject.GetComponent<Rigidbody2D>().velocity = Velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFlicker)
        {
            LightManager.Instance.CreateLight(transform.position, LightPower);
            nextFlicker = Time.time + FlickerTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.ResetLevel(); // Die, biatch
            }
            Destroy(gameObject);
        }
    }
}
