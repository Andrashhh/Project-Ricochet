using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ricochet;
using Ricochet.KinematicC;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Rendering.VirtualTexturing;
using Unity.VisualScripting;
using Ricochet.Input;

namespace Ricochet.KinematicC
{
    public class Player : MonoBehaviour
    {
        PlayerInputHandler m_PlayerInputHandler;

        public CharacterController Character;
        public CharacterCamera CharacterCamera;

        private void Awake() {

        }

        private void Start()
        {
            m_PlayerInputHandler = PlayerInputHandler.Instance;

            Cursor.lockState = CursorLockMode.Locked;

            // Tell camera to follow transform
            CharacterCamera.SetFollowTransform(Character.CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            CharacterCamera.IgnoredColliders.Clear();
            CharacterCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            if (m_PlayerInputHandler.m_FireInput)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            HandleCharacterInput();
        }

        private void LateUpdate()
        {

            HandleCameraRotating();
            HandleCameraInput();
        }

        private void HandleCameraRotating() {
            // Handle rotating the camera along with physics movers
            if(CharacterCamera.RotateWithPhysicsMover && Character.Motor.AttachedRigidbody != null) {
                CharacterCamera.PlanarDirection = Character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * CharacterCamera.PlanarDirection;
                CharacterCamera.PlanarDirection = Vector3.ProjectOnPlane(CharacterCamera.PlanarDirection, Character.Motor.CharacterUp).normalized;
            }
        }

        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            float mouseLookAxisUp = m_PlayerInputHandler.m_LookInput.y;
            float mouseLookAxisRight = m_PlayerInputHandler.m_LookInput.x;
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            //float scrollInput = -Input.GetAxis(MouseScrollInput);
            float scrollInput = 0;


            // Apply inputs to the camera
            CharacterCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = m_PlayerInputHandler.m_MoveInput.y;
            characterInputs.MoveAxisRight = m_PlayerInputHandler.m_MoveInput.x;
            characterInputs.CameraRotation = CharacterCamera.Transform.rotation;
            characterInputs.JumpDown = m_PlayerInputHandler.m_JumpInput;
            characterInputs.CrouchDown = m_PlayerInputHandler.m_CrouchInput;
            characterInputs.CrouchUp = m_PlayerInputHandler.m_CrouchInput;

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }
    }
}