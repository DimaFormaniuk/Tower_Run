using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;

    private Animator _animator;

    public Transform FixationPoint => _fixationPoint;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.SetBool("isRunning", true);
    }

    public void StopRun()
    {
        _animator.SetBool("isRunning", false);
    }

    public void Kicking(bool value)
    {
        _animator.SetBool("isKicking", value);
    }

    public void Texting(bool value)
    {
        _animator.SetBool("isTexting", value);
    }

    public void Bounce(float force, Vector3 center, float radius)
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.AddExplosionForce(force, center, radius);
        }
    }
}
