using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speedPosX = 3;
    private Rigidbody2D _myRg2D;

    [Range(1, 10)]
    public float jumpVelocity = 10f;

    private Checkpoint _lastCheckpoint;
    public LightShieldBehaviour overloadScript;

    private bool _isGrounded = true;
    private bool _isFacingRight = true;

    private float _lastHorizontalAxis;

    
    [Header("Ground rules")]
    public LayerMask whatIsGround;
    public float groundRadius;
    public Transform groundPoint;
    
    private Animator _animator;
    
    private void Awake() {
        _myRg2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
        Flip();
        
        _animator.SetBool("Grounded", _isGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(_myRg2D.velocity.x));
        Debug.Log(_myRg2D.velocity.y);
        _animator.SetFloat("SpeedV", _myRg2D.velocity.y);
    }

    public void Move(float horizontalAxis) {
        // float distanceX = Time.deltaTime * horizontalAxis * speedPosX;

        //transform.Translate(distanceX, 0, 0);
        _lastHorizontalAxis = horizontalAxis;
        _myRg2D.velocity = new Vector2(horizontalAxis * speedPosX, _myRg2D.velocity.y);
        
//        Debug.Log(Mathf.Abs(_myRg2D.velocity.x));
    }

    public void Jump() {
        if (_isGrounded == false){
            return;
        }Debug.Log("collision");
        _myRg2D.velocity = Vector2.up * jumpVelocity;
    }

    public void Overload()
    {
        if (!overloadScript.IsOverloaded)
            overloadScript.Overload();
    }

    public void SetLastCheckpoint(Checkpoint cp) {
        _lastCheckpoint = cp;
    }

    private void Flip()
    {
        if ((_lastHorizontalAxis < 0 && _isFacingRight) || (_lastHorizontalAxis > 0 && !_isFacingRight))
        {
            _isFacingRight = !_isFacingRight;
            var localTransform = transform;
            Vector3 theScale = localTransform.localScale;
            theScale.x *= -1;
            localTransform.localScale = theScale;
        }
    }
}
