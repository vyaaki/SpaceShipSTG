using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;

    [SerializeField] private GameObject leftSpawner;
    [SerializeField] private GameObject rightSpawner;
    
    private float nextLaunchTime;
    [SerializeField] private bool isRightSide;
    [SerializeField] private float enemySpeed;
    private GameObject spawnZone, destZone;
    void Update()
    {
        if (Time.time > nextLaunchTime)
        {
            
            if (!isRightSide)
            {
                spawnZone = leftSpawner;
                destZone = rightSpawner;
            }
            else
            {
                spawnZone = rightSpawner;
                destZone = leftSpawner;
            }
            var xScale = spawnZone.transform.localScale.x;
            var position = spawnZone.transform.position;
            float buttomEdge = position.z - (xScale / 2);
            float upEdge = position.z + (xScale / 2);
            var spawnPositionZ = Random.Range(buttomEdge, upEdge);
            Vector3 spawnPosition = new Vector3(position.x, position.y, spawnPositionZ);
            
            xScale = destZone.transform.localScale.x;
            position = destZone.transform.position;
            buttomEdge = position.y - (xScale / 2);
            upEdge = position.y + (xScale / 2);
            var destPositionZ = Random.Range(buttomEdge, upEdge);
            Vector3 destPosition = new Vector3(position.x, position.y, destPositionZ);

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.LookAt(destPosition);
            enemy.GetComponent<Rigidbody>().velocity = (destPosition - enemy.transform.position).normalized * enemySpeed;

            isRightSide = !isRightSide;
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }

    }
}
