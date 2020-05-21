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
    private void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        float speed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, -20) * speed;

        float size = Random.Range(minSize, maxSize);
        asteroid.transform.localScale *= size;
        asteroidExplosion.transform.localScale *= size;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(UnityConstants.Tags.Asteroid) || other.CompareTag(UnityConstants.Tags.GameBoundary))
        {
            return;
        }
        Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        if (other.CompareTag(UnityConstants.Tags.Player))
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
