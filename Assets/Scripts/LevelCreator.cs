using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private Amplifier _amplifierTemplate;
    [SerializeField] private Obstacle _obstacleTemplate;
    [SerializeField] private int _humanTowerCount;
    [SerializeField] private float _distanceToAmplifier;
    [SerializeField] private float _distanceToObstacle;

    private void Start()
    {
        GeneratorLevel();
    }

    private void GeneratorLevel()
    {
        float roadLength = _pathCreator.path.length;
        float distanceBetweenTower = roadLength / _humanTowerCount;

        float distanceTravelled = 0;
        Vector3 spawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTravelled += distanceBetweenTower;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

            Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);

            GeneratorPrefab(distanceTravelled, _distanceToAmplifier, _amplifierTemplate.gameObject);

            //if (Random.Range(0, 100) % 2 == 0)
            //{
            //    GeneratorPrefab(distanceTravelled, _distanceToObstacle, _obstacleTemplate.gameObject);
            //}
        }
    }

    private void GeneratorPrefab(float distanceTravelled, float distanceToPrefab, GameObject prefab)
    {
        distanceTravelled += distanceToPrefab;
        Vector3 spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}
