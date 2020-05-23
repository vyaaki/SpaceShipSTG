using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private GameObject asteroidExplosion;
    [SerializeField] private GameObject playerExplosion;

    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    private float size;
    private void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        float speed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, -20) * speed;

        size = Random.Range(minSize, maxSize);
        asteroid.transform.localScale *= size;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(UnityConstants.Tags.Asteroid) || other.CompareTag(UnityConstants.Tags.GameBoundary))
        {
            return;
        }
        GameObject explosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale *= size;
        if (other.CompareTag(UnityConstants.Tags.Player))
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
