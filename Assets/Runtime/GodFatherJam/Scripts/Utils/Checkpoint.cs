using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour {
    [SerializeField] private float overloadChargesToRefill;
    [SerializeField] private Vector2 spawnPosition;
    
    private void Awake() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().SetLastCheckpoint(this);
    }

    public float GetOverloadChargesToRefill() { return overloadChargesToRefill; }

    public Vector2 GetSpawnPosition() { return transform.TransformPoint(spawnPosition); }

    private void OnDrawGizmos() {
        float gizmosCubeSize = 0.3f;
        
        Gizmos.color = Color.yellow;
        Vector3 spawn = GetSpawnPosition();
        Gizmos.DrawCube(spawn, Vector3.one * gizmosCubeSize);
    }
}