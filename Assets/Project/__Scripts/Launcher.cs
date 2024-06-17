using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ricochet.Input;

namespace Ricochet
{
    public class Launcher : MonoBehaviour
    {
        PlayerInputHandler m_PlayerInputHandler;

        public GameObject m_LaunchPoint;
        public GameObject m_ProjectilePrefab;


        void Awake() {

        }

        void Start() {
            m_PlayerInputHandler = PlayerInputHandler.Instance;
        }

        void Update() {
            if (m_PlayerInputHandler.m_FireAltInput) {
                Instantiate(m_ProjectilePrefab, m_LaunchPoint.transform.position, Quaternion.identity);
            }
        }
    }
}
