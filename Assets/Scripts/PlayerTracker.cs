using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerTower _playerTower;
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Vector3 _offsetRotation;

    private void OnEnable()
    {
        _playerTower.HumanAdded += OnHumanAdded;
    }

    private void OnDisable()
    {
        _playerTower.HumanAdded -= OnHumanAdded;
    }

    private void Start()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = _playerTower.transform.position;
        transform.localPosition += _offsetPosition;

        Vector3 loolAtPoint = _playerTower.transform.position + _offsetRotation;

        transform.LookAt(loolAtPoint);
    }

    private void OnHumanAdded(int count)
    {
        _offsetPosition += (Vector3.up + Vector3.back) * count;
        UpdatePosition();
    }
}
