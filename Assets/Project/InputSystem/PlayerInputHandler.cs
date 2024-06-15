using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ricochet.Input {
    public class PlayerInputHandler : MonoBehaviour {
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset _playerControls;

        [Header("Action Map Name References")]
        [SerializeField] private string _actionMapName = "Player";

        [Header("Action Name References")]
        [SerializeField] private string _move = "Move";
        [SerializeField] private string _look = "Look";
        [SerializeField] private string _fire = "Fire";
        [SerializeField] private string _jump = "Jump";
        [SerializeField] private string _crouch = "Crouch";
        [SerializeField] private string _sprint = "Sprint";

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _fireAction;
        private InputAction _jumpAction;
        private InputAction _crouchAction;
        private InputAction _sprintAction;

        public Vector2 m_MoveInput { get; private set; }
        public Vector2 m_LookInput { get; private set; }
        public bool m_FireInput { get; private set; }
        public bool m_JumpInput { get; private set; }
        public bool m_CrouchInput { get; private set; }
        public float m_SprintInput { get; private set; }


        public static PlayerInputHandler Instance { get; private set; }

        void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }

            _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_move);
            _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_look);
            _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_fire);
            _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_jump);
            _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_crouch);
            _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_sprint);
            RegisterInputActions();
        }

        void RegisterInputActions() {
            _moveAction.performed += ctx => m_MoveInput = ctx.ReadValue<Vector2>();
            _moveAction.canceled += ctx => m_MoveInput = Vector2.zero;

            _lookAction.performed += ctx => m_LookInput = ctx.ReadValue<Vector2>();
            _lookAction.canceled += ctx => m_LookInput = Vector2.zero;

            //_jumpAction.triggered += ctx => m_JumpInput = true;
            //_jumpAction.performed += ctx => m_JumpInput = false;
            m_JumpInput = _jumpAction.triggered;
            m_FireInput = _fireAction.triggered;
            m_CrouchInput = _crouchAction.triggered;

            _sprintAction.performed += ctx => m_SprintInput = ctx.ReadValue<float>();
            _sprintAction.canceled += ctx => m_SprintInput = 0f;
        }

        void OnEnable() {
            _moveAction.Enable();
            _lookAction.Enable();
            _fireAction.Enable();
            _jumpAction.Enable();
            _crouchAction.Enable();
            _sprintAction.Enable();
        }
        void OnDisable() {
            _moveAction.Disable();
            _lookAction.Disable();
            _fireAction.Disable();
            _jumpAction.Disable();
            _crouchAction.Disable();
            _sprintAction.Disable();
        }
    }
}
