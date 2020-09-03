using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Liane : MonoBehaviour {
    public IActivable objectToActivate;
    public void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Light")) return;
        
        LightShieldBehaviour lightShieldBehaviour = other.GetComponent<LightShieldBehaviour>();

        if (!lightShieldBehaviour.IsOverloaded) return;
            
        objectToActivate?.Activate();
        enabled = false;
    }
}