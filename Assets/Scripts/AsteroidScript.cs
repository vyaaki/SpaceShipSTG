using System.Linq;
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
        if (new []{UnityConstants.Tags.Asteroid, UnityConstants.Tags.GameBoundary,UnityConstants.Tags.EnemyLazerShoot, UnityConstants.Tags.Enemy}.Contains(other.tag) )
        {
            return;
        }
        GameObject explosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale *= size;
        if (other.CompareTag(UnityConstants.Tags.Player))
        {
            PlayerScript player = GameObject.FindObjectOfType<PlayerScript>();
            player.DestroyPlayer();
        }
        Destroy(gameObject);
        if (other)
        {
            Destroy(other.gameObject);
        }
    }
}
