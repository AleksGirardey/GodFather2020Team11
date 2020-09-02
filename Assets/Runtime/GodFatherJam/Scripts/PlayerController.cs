using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // public bool _isMoving;
    public float speedPosX = 3;
    private Rigidbody2D _myRg2D;

    [Range(1, 10)]
    public float jumpVelocity = 10f;

    private void Awake()
    {
        _myRg2D = GetComponent<Rigidbody2D>();
    }
   
    private void Update(){
        Move();
        Jump();
    }

    private void Move() {
        if (!(Input.GetAxis("Horizontal") <= 0f) && !(Input.GetAxis("Horizontal") >= 0f)) return;

        //     _isMoving = true;
        // } else {
        //     _isMoving = false;
        // }

        // if (!_isMoving) return;

        float distanceX = Time.deltaTime * Input.GetAxis("Horizontal") * speedPosX;

        transform.Translate(distanceX, 0, 0);
    }

    private void Jump(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            _myRg2D.velocity = Vector2.up * jumpVelocity;
        }
    }

}
