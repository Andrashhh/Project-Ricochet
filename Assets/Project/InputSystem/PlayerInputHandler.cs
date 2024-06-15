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
        [SerializeField] private string _fireAlt = "FireAlt";
        [SerializeField] private string _jump = "Jump";
        [SerializeField] private string _crouch = "Crouch";
        [SerializeField] private string _sprint = "Sprint";

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _fireAction;
        private InputAction _fireAltAction;
        private InputAction _jumpAction;
        private InputAction _crouchAction;
        private InputAction _sprintAction;

        public Vector2 m_MoveInput { get; private set; }
        public Vector2 m_LookInput { get; private set; }
        public bool m_FireInput { get; private set; }
        public bool m_FireAltInput { get; private set; }
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
            _lookAction = _playerControls.FindActionMap(_actionMapName).FindAction(_look);
            _fireAction = _playerControls.FindActionMap(_actionMapName).FindAction(_fire);
            _fireAltAction = _playerControls.FindActionMap(_actionMapName).FindAction(_fireAlt);
            _jumpAction = _playerControls.FindActionMap(_actionMapName).FindAction(_jump);
            _crouchAction = _playerControls.FindActionMap(_actionMapName).FindAction(_crouch);
            _sprintAction = _playerControls.FindActionMap(_actionMapName).FindAction(_sprint);
            RegisterInputActions();
        }

        void Update() {
            Debug.Log(m_JumpInput);

            m_JumpInput = _jumpAction.triggered;
            m_FireInput = _fireAction.triggered;
            m_FireAltInput = _fireAltAction.triggered;
        }

        void RegisterInputActions() {
            _moveAction.performed += ctx => m_MoveInput = ctx.ReadValue<Vector2>();
            _moveAction.canceled += ctx => m_MoveInput = Vector2.zero;

            _lookAction.performed += ctx => m_LookInput = ctx.ReadValue<Vector2>();
            _lookAction.canceled += ctx => m_LookInput = Vector2.zero;

            _crouchAction.performed += ctx => m_CrouchInput = true;
            _crouchAction.canceled += ctx => m_CrouchInput = false;

            _sprintAction.performed += ctx => m_SprintInput = ctx.ReadValue<float>();
            _sprintAction.canceled += ctx => m_SprintInput = 0f;
        }

        void OnEnable() {
            _moveAction.Enable();
            _lookAction.Enable();
            _fireAction.Enable();
            _fireAltAction.Enable();
            _jumpAction.Enable();
            _crouchAction.Enable();
            _sprintAction.Enable();
        }
        void OnDisable() {
            _moveAction.Disable();
            _lookAction.Disable();
            _fireAction.Disable();
            _fireAltAction.Disable();
            _jumpAction.Disable();
            _crouchAction.Disable();
            _sprintAction.Disable();
        }
    }
}
