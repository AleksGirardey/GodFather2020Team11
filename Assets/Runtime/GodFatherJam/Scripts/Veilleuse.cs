using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Veilleuse : MonoBehaviour
{
    public GameObject objectToActivate;
    public void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("light")) return;

        LightShieldBehaviour lightShieldBehaviour = other.GetComponent<LightShieldBehaviour>();

        if (!lightShieldBehaviour.IsOverloaded) return;

        objectToActivate?.SetActive(true);
        enabled = false;
    }
}
