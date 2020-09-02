using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterController : MonoBehaviour
{
    public int playerId;
    private Player _player;

    private Vector3 moveVector;
    private bool _interaction;
    private bool _overload;

    private void Awake()
    {
        // Get the Player for a particular playerId
        _player = ReInput.players.GetPlayer(playerId);
    }

    // Update is called once per frame
    private void Update()
    {
        //if(Input.GetAxis("Horizontal") < 0)
        //{
        //    //move left
        //    Debug.Log("left");
        //}

        //if (Input.GetAxis("Horizontal") > 0)
        //{
        //    //Move right
        //    Debug.Log("right");
        //}

        //if (Input.GetAxis("Vertical") > 0)
        //{
        //    //Move up
        //    Debug.Log("up");
        //}

        //if (Input.GetAxis("Vertical") < 0)
        //{
        //    //Move down
        //    Debug.Log("down");
        //}

        GetInput();
        ProcessInput();
    }


    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = _player.GetAxis("MoveHorizontal"); // get input by name or action id
        moveVector.y = _player.GetAxis("MoveVertical");
        _overload = _player.GetButtonDown("Overload");
        _interaction = _player.GetButtonDown("Interaction");
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            //cc.Move(moveVector * moveSpeed * Time.deltaTime);
            Debug.Log("Movement x :" + moveVector.x + " Movement y :" + moveVector.y);
        }

        if (_overload)
        {
            Debug.Log("Overload");
        }

        if (_interaction)
        {
            Debug.Log("Interaction");
        }
    }
}
