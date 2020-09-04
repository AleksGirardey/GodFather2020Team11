using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
    public UnityEvent eventToTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            eventToTrigger.Invoke();
    }
}
