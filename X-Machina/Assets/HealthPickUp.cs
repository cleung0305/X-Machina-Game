﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    HealthSystem healthPickup;
    Player player;
    void Awake()
    {
        healthPickup = FindObjectOfType<HealthSystem>();
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (healthPickup.playerHealth < 5)
            {
                Destroy(gameObject);
                healthPickup.playerHealth += 1;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        if (healthPickup.playerHealth < 3)
    //        {
    //            Destroy(gameObject);
    //            healthPickup.playerHealth += 1;
    //        }
    //    }
    //}
}
