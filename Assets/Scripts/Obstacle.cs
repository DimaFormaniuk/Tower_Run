using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Vector2Int _rangeInObstacleHeight;

    private float _valueHeight;

    public int Value => (int)_valueHeight;

    private void Start()
    {
        _valueHeight = Random.Range(_rangeInObstacleHeight.x, _rangeInObstacleHeight.y);
        CalculateSize();
    }

    private void CalculateSize()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * _valueHeight, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.localScale.y / 2f, transform.position.z);
    }
}
