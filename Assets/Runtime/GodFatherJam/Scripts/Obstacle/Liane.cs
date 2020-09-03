using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Liane : MonoBehaviour {
    public Activable objectToActivate;
    public void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Light")) return;
        
        LightShieldBehaviour lightShieldBehaviour = other.GetComponent<LightShieldBehaviour>();

        if (!lightShieldBehaviour.IsOverloaded) return;
            
        if (objectToActivate != null)
            objectToActivate.Activate();
        gameObject.SetActive(false);
    }
}