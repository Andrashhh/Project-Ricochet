using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ricochet
{
    public class Projectile : MonoBehaviour
    {
        GameObject _startObject;
        GameObject _targetObject;
        public GameObject _headObject;
        Vector3 _targetDirection;
        
        [SerializeField] float _speed = 4;
        [SerializeField] float _rotateSpeed = 4;



        Vector3 velocity = Vector3.zero;

        Rigidbody _rb;

        void Awake() {
            _startObject = gameObject;
            _targetObject = GameObject.FindGameObjectWithTag("KokTarget");
            _rb = GetComponent<Rigidbody>();
            
        }

        void FixedUpdate() {
            HomingLogic();
        }

        void HomingLogic() {
            Vector3 distance = (_targetObject.transform.position - transform.position).normalized;
            Vector3 finalDistance = (distance - transform.forward);

            _rb.velocity += (transform.forward * _speed) + (distance * (_speed * 0.98f) + (finalDistance * (_speed * 0.98f) * _rotateSpeed));
            _rb.MoveRotation(Quaternion.LookRotation(_rb.velocity, Vector3.up));
        }

        void OnCollisionEnter(Collision collision) {
            if (collision.collider.gameObject.CompareTag("Player")) Destroy(gameObject);
        }
    }
}

