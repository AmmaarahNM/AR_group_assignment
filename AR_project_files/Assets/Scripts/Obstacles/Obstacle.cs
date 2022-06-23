using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float _speed;
    private Vector3 _movementVector;
    private void Start()
    {
        _movementVector = -transform.right * _speed;
        Destroy(gameObject, 8f);
    }

    private void FixedUpdate()
    {
        transform.position += _movementVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
