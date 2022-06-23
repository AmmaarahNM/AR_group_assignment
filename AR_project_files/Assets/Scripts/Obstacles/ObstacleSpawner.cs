using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private ObstacleController _obstacleController;

    private void Start()
    {
        _obstacleController = GetComponentInParent<ObstacleController>();
    }

    public void SpawnObstacle()
    {
        Instantiate(_obstacleController.GetRandomObstacle(), transform.position, transform.rotation);
    }
}
