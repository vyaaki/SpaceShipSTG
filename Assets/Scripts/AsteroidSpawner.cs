using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroid;
    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;

    private float nextLaunchDelay;

    private void Update()
    {
        var position = transform.position;
        float positionZ = position.z;
        float xScale = transform.localScale.x;
        float positionX = Random.Range(-xScale / 2, xScale / 2);
        if (Time.time > nextLaunchDelay)
        {
            Instantiate(asteroid, new Vector3(positionX, 0, positionZ), Quaternion.identity);
            nextLaunchDelay = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
