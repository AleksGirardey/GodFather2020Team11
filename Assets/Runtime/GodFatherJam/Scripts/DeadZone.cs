﻿using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeadZone : MonoBehaviour {
    private void Awake() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().ReSpawn();
        }
    }
}
