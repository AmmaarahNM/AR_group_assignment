using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] List<ObstacleSpawner> _spawners = new List<ObstacleSpawner>();
    private float _timer;

    private void Start()
    {
        _timer = Random.Range(0.1f, 5f);
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            _spawners[Random.Range(0, _spawners.Count)].SpawnObstacle();
            _timer = Random.Range(1f, 5f);
        }

        _timer -= 1 * Time.deltaTime;
    }

    [SerializeField] List<GameObject> obstacles = new List<GameObject>();

    public GameObject GetRandomObstacle()
    {
        GameObject randomObstacle = null;

        randomObstacle = obstacles[Random.Range(0,obstacles.Count)];

        return randomObstacle;
    }
}
