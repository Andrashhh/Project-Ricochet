using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Ricochet
{
    public class Projectile : MonoBehaviour
    {
        GameObject _startObject;
        GameObject _targetObject;
        Vector3 _targetDirection; 
        float _speed;

        Rigidbody _rb;

        void Awake() {
            _startObject = gameObject;
            _targetObject = GameObject.FindGameObjectWithTag("Player");
            _rb = GetComponent<Rigidbody>();
        }

        void Update() {
            _targetDirection = _targetObject.transform.position - _startObject.transform.position;

            _rb.AddForce(_targetDirection, ForceMode.Force);
            Debug.DrawRay(_targetObject.transform.position, _targetDirection, Color.green);
        }

        void OnCollisionEnter(Collision collision) {
            if (collision.collider.gameObject.CompareTag("Player")) Destroy(gameObject);
        }
    }
}
