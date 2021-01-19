using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private bool _isGrounded;
    private Rigidbody _rigidbody;
    private float _multiplier;

    private void Start()
    {
        _multiplier = 1;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isGrounded == true)
        {
            _isGrounded = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce * _multiplier);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Road road))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Amplifier amplifier))
        {
            _multiplier = amplifier.Value;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Amplifier amplifier))
        {
            _multiplier = 1;
        }
    }
}
