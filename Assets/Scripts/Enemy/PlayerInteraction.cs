using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class PlayerInteraction : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<Player>() != null)
                GameManager.Instance.ResetLevel();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Player>() != null)
                GameManager.Instance.ResetLevel();
        }

    }
}
