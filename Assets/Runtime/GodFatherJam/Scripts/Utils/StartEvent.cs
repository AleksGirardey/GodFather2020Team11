using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
    public UnityEvent eventToTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        eventToTrigger.Invoke();
    }
}
