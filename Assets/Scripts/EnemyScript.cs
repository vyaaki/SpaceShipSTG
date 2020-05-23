using UnityConstants;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject lazerShoot;
    [SerializeField] private Transform lazerSpawner;
    private float nextShootTime;
    private GameObject player;
    private PlayerScript playerScript;
    [SerializeField] private GameObject shipExplosion;
    [SerializeField] private float shootDelay;
    [SerializeField] private float shootSpeed;

    private void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        if (playerScript) player = playerScript.gameObject;
    }

    private void Update()
    {
        if (Time.time > nextShootTime && player)
        {
            var enemyShoot = Instantiate(lazerShoot, lazerSpawner.position, Quaternion.identity);
            var position = player.transform.position;
            enemyShoot.transform.LookAt(position);

            enemyShoot.GetComponent<Rigidbody>().velocity =
                (position - enemyShoot.transform.position).normalized * shootSpeed;

            nextShootTime = Time.time + shootDelay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            playerScript.DestroyPlayer();
        }
        else if (other.CompareTag(Tags.PlayerLazer))
        {
            Destroy(gameObject);
            Instantiate(shipExplosion, gameObject.transform.position, Quaternion.identity);
        }
    }
}