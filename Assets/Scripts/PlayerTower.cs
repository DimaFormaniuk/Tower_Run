using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceCheker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkCollider;

    private List<Human> _humans;

    public event UnityAction<int> HumanAdded;

    private bool _obstacleBool;

    private void Start()
    {
        _humans = new List<Human>();
        Vector3 spawnPoint = transform.position;

        _humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));

        _humans[0].Run();
        HumanAdded?.Invoke(_humans.Count);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();

            if (collisionTower != null)
            {
                List<Human> collectedHumans = collisionTower.CollectHuman(_distanceCheker, _fixationMaxDistance);

                if (collectedHumans != null)
                {
                    _humans[0].StopRun();

                    for (int i = collectedHumans.Count - 1; i >= 0; i--)
                    {
                        Human insertHuman = collectedHumans[i];
                        InsertHuman(insertHuman);

                        //DisplaceCheckers(insertHuman);
                        DisplaceCheckers();
                    }

                    HumanAdded?.Invoke(_humans.Count);
                    _humans[0].Run();
                }

                collisionTower.Break();
            }
        }
    }

    private void InsertHuman(Human collectedHumans)
    {
        _humans.Insert(0, collectedHumans);
        SetHumanPosition(collectedHumans);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }

    private void DisplaceCheckers(Human human)
    {
        float displaceScale = 1.5f;
        Vector3 distanceCheckerNewPosition = _distanceCheker.position;
        distanceCheckerNewPosition.y -= human.transform.localScale.y * displaceScale;
        _distanceCheker.position = distanceCheckerNewPosition;
        _checkCollider.center = _distanceCheker.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            _obstacleBool = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            if (_obstacleBool == true)
            {
                DestroyHuman(obstacle.Value);
                _obstacleBool = false;
            }
        }
    }

    private void DestroyHuman(int count)
    {
        _humans[0].StopRun();

        for (int i = 0; i < count; i++)
        {
            Destroy(_humans[0].gameObject);
            _humans.RemoveAt(0);
        }

        DisplaceCheckers(_humans[0]);

        HumanAdded?.Invoke(_humans.Count);
        _humans[0].Run();
    }

    private void DisplaceCheckers()
    {
        _distanceCheker.position = _humans[0].transform.position;
        _checkCollider.center = _distanceCheker.localPosition;
    }

}
