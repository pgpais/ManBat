using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TrapDetector : MonoBehaviour
{
    [SerializeField] private Color warningColor;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField]
    private Vector2 trapCheckSize;
    [SerializeField]
    private LayerMask layerMask;
    
    
    private float _maxDist;
    private Color _baseColor;
    

    private void Start()
    {
        _maxDist = (trapCheckSize.x / 2f * trapCheckSize.x / 2f) + (trapCheckSize.y / 2f * trapCheckSize.y / 2f);
        _baseColor = playerRenderer.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForTraps();
    }

    private void CheckForTraps()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, trapCheckSize, 0f, layerMask);
        if (hit != null)
        {
            var position = transform.position;
            float dist = (hit.ClosestPoint(position) - (Vector2)position).magnitude;
            float dangerRatio = 1 - (dist*dist)/_maxDist;
            Debug.Log("Trap close! " + dangerRatio);
            playerRenderer.color = Color.Lerp(_baseColor, warningColor, dangerRatio);
        }
        else
        {
            playerRenderer.color = _baseColor;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Trap") && other.CompareTag("Enemy"))
        {
            float distToDanger = (other.transform.position - transform.position).magnitude;
            //Debug.Log("Danger close! " + distToDanger);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta - new Color(0,0,0,0.9f);
        Gizmos.DrawCube(transform.position, trapCheckSize);
    }
}
