using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speedPosX = 3;
    private Rigidbody2D _myRg2D;

    [Range(1, 10)]
    public float jumpVelocity = 10f;

    private Checkpoint _lastCheckpoint;
    
    private void Awake() {
        _myRg2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalAxis) {
        float distanceX = Time.deltaTime * horizontalAxis * speedPosX;

        transform.Translate(distanceX, 0, 0);
    }

    public void Jump() {
        _myRg2D.velocity = Vector2.up * jumpVelocity;
    }

    public void SetLastCheckpoint(Checkpoint cp) {
        _lastCheckpoint = cp;
    }
}
