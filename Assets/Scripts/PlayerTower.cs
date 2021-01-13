using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceCheker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkCollider;

    private List<Human> _humans;

    private void Start()
    {
        _humans = new List<Human>();
        Vector3 spawnPoint = transform.position;
        _humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();

            List<Human> collectedHumans = collisionTower.CollectHuman(_distanceCheker, _fixationMaxDistance);

            if (collectedHumans != null)
            {
                InsertHuman(collectedHumans);
            }
        }
    }

    private void InsertHuman(List<Human> collectedHumans)
    {
        for (int i = collectedHumans.Count - 1; i >= 0; i--)
        {
            Human insertHuman = collectedHumans[i];
            _humans.Insert(0, insertHuman);
            SetHumanPosition(insertHuman);
        }
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0,human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }
}
