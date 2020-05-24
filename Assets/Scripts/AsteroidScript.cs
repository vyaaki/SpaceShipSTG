using System.Linq;
using UnityConstants;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private GameObject asteroidExplosion;

    [SerializeField] private int asteroidScore;
    [SerializeField] private float maxSize;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float minSize;

    [SerializeField] private float minSpeed;
    [SerializeField] private GameObject playerExplosion;
    [SerializeField] private float rotationSpeed;
    private float size;

    private void Start()
    {
        var asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        var speed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, -20) * speed;

        size = Random.Range(minSize, maxSize);
        asteroid.transform.localScale *= size;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (new[] {Tags.Asteroid, Tags.GameBoundary, Tags.EnemyLazerShoot, Tags.Enemy}.Contains(other.tag)) return;
        var explosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale *= size;
        if (other.CompareTag(Tags.Player))
        {
            var player = FindObjectOfType<PlayerScript>();
            player.DestroyPlayer();
        }
        else
        {
            GameControllerScript.instance.IncreaseScore(asteroidScore);
        }

        Destroy(gameObject);
        if (other) Destroy(other.gameObject);
    }
}