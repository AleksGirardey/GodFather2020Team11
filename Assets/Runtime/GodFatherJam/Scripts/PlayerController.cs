using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool _isMoving, _isJumping;
    public float speedPosX = 3;
    public float _speedPosY = 2;
    private Rigidbody2D _myRg2D;

    [Range(1, 10)]
    public float jumpVelocity;

    void Start(){
        _myRg2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Move();
        Jump();
    }

    void Move(){
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            _isMoving = true;
        }else{
            _isMoving = false;
        }

        if (_isMoving){
            float distanceX = Time.deltaTime * Input.GetAxis("Horizontal") * speedPosX;

            transform.Translate(distanceX, 0, 0);
            _isJumping = true;
        }
    }

    void Jump(){
            if (Input.GetKeyDown(KeyCode.Space)){
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
            }
    }

}
