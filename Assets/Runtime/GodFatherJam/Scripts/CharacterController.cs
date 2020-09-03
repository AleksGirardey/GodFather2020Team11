using System;
using UnityEngine;
using Rewired;

namespace GodFather
{

    public class CharacterController : MonoBehaviour
    {
        public static CharacterController Instance;

        public int playerId;
        private Player _player;

        public PlayerController playerController;

        private Animator _animator; 
        
        private Vector3 _moveVector;
        private bool _interaction;
        private bool _overload;
        private bool _jump;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            if (Instance != this) Destroy(gameObject);

            // Get the Player for a particular playerId
            _player = ReInput.players.GetPlayer(playerId);
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            GetInput();
            ProcessInput();
        }


        private void GetInput() {
            // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
            // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

            _moveVector.x = _player.GetAxis("MoveHorizontal"); // get input by name or action id
            _moveVector.y = _player.GetAxis("MoveVertical");
            _overload = _player.GetButtonDown("Overload");
            _interaction = _player.GetButtonDown("Interaction");
            _jump = _player.GetButtonDown("Jump") && (_moveVector.y >= 0.0f);
        }

        public bool IsInteracting()
        {
            return _interaction;
        }

        public bool HasReleaseFallingFromPlatform()
        {
            return _moveVector.y < 0.0f && _player.GetButtonUp("Jump");
        }

        public bool IsFallingFromPlatform() {
            return _moveVector.y < 0.0f && _jump;
        }

        private void ProcessInput()
        {
            // Process movement
            if (_moveVector.x > 0.0f || _moveVector.x < 0.0f)
            {
                playerController.Move(_moveVector.x);
            }

            if (_jump)
            {
                playerController.Jump();
            }

            if (_overload)
            {
                playerController.Overload();
            }

            if (_interaction)
            {
                Debug.Log("Interact");
            }
        }


    }
}
