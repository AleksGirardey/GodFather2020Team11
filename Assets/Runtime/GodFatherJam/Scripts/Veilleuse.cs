using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Veilleuse : MonoBehaviour
{
    public GameObject light;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Light")) return;

        LightShieldBehaviour lightShieldBehaviour = other.GetComponent<LightShieldBehaviour>();

        if (!lightShieldBehaviour.IsOverloaded) return;

        light?.SetActive(true);
    }
}
