using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterController : MonoBehaviour
{
    public int playerId;
    private Player _player;

    public PlayerController playerController;

    private Vector3 _moveVector;
    private bool _interaction;
    private bool _overload;
    private bool _jump;

    private void Awake()
    {
        // Get the Player for a particular playerId
        _player = ReInput.players.GetPlayer(playerId);
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        ProcessInput();
    }


    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        _moveVector.x = _player.GetAxis("MoveHorizontal"); // get input by name or action id
        _moveVector.y = _player.GetAxis("MoveVertical");
        _overload = _player.GetButtonDown("Overload");
        _interaction = _player.GetButtonDown("Interaction");
        _jump = _player.GetButtonDown("Jump");
    }

    private void ProcessInput()
    {
        // Process movement
        if (_moveVector.x != 0.0f)
        {
            playerController.Move(_moveVector.x);
        }

        if (_moveVector.y < 0.0f && _jump){
            //go under platform
            Debug.Log("UnderPlatform");
        }
        else if (_jump)
        {
            playerController.Jump();
            Debug.Log("Jump");
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
