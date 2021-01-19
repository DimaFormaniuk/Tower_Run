using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Vector2Int _humanInTowerRange;
    [SerializeField] private Human[] _humansTemplates;

    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;

    private List<Human> _humanInTower;

    public int HumanInTower => humanInTowerCount;

    private int humanInTowerCount;

    private void Start()
    {
        _humanInTower = new List<Human>();
        humanInTowerCount = Random.Range(_humanInTowerRange.x, _humanInTowerRange.y);
        SpawnHumans(humanInTowerCount);
    }

    private void SpawnHumans(int humanCount)
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < humanCount; i++)
        {
            Human spawnedHuman = _humansTemplates[Random.Range(0, _humansTemplates.Length)];

            _humanInTower.Add(Instantiate(spawnedHuman, spawnPoint, Quaternion.identity, transform));

            _humanInTower[i].transform.localPosition = new Vector3(0, _humanInTower[i].transform.localPosition.y, 0);

            spawnPoint = _humanInTower[i].FixationPoint.position;

            SomeAnimation(_humanInTower[i]);
        }
    }

    public List<Human> CollectHuman(Transform distanceCheker, float fixationMaxDistance)
    {
        for (int i = 0; i < _humanInTower.Count; i++)
        {
            float distanceBetweenPoint = CheckDistanceY(distanceCheker, _humanInTower[i].FixationPoint.transform);

            if (distanceBetweenPoint < fixationMaxDistance)
            {
                List<Human> collectionHumans = _humanInTower.GetRange(0, i + 1);
                _humanInTower.RemoveRange(0, i + 1);                

                return collectionHumans;
            }
        }

        return null;
    }

    private float CheckDistanceY(Transform distanceChecker, Transform humanFixationPoint)
    {
        Vector3 distanceChecherY = new Vector3(0, distanceChecker.position.y, 0);
        Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);

        return Vector3.Distance(distanceChecherY, humanFixationPointY);
    }

    public void Break()
    {
        for (int i = 0; i < _humanInTower.Count; i++)
        {
            Vector3 center = _humanInTower[i].transform.position;

            if (Random.Range(0, 100) % 2 == 0)
            {
                center += Vector3.right;
            }
            else
            if (Random.Range(0, 100) % 2 == 0)
            {
                center += Vector3.left;
            }
            _humanInTower[i].transform.parent = null;
            _humanInTower[i].Bounce(_bounceForce, center, _bounceRadius);
        }

        Destroy(gameObject);
    }

    private void SomeAnimation(Human human)
    {
        int n = Random.Range(0, 3);

        human.Texting(n == 1);
        human.Kicking(n == 2);
    }
}
