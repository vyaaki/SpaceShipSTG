using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody playerShip;
    [SerializeField] private float speed = 20;
    [SerializeField] private float xMinBorder;
    [SerializeField] private float xMaxBorder;
    [SerializeField] private float zMinBorder;
    [SerializeField] private float zMaxBorder;
    [SerializeField] private float tilt;

    [SerializeField] private GameObject lazerShot;
    [SerializeField] private GameObject smallLazerShot;
    [SerializeField] private Transform lazerGun;
    [SerializeField] private List<Transform> smallLazers;
    [SerializeField] private float shotDelay;
    
    [SerializeField] float lazerSpeed;
    private float nextShotTime, smallShotDelay, nextSmallShotTime;

    private const int SMALL_LAZER_DIVIDER = 4;
    private const int SMALL_LAZER_DELAY_DIVIDER = 2;
    void Start()
    {
        playerShip = GetComponent<Rigidbody>();
        smallShotDelay = shotDelay / SMALL_LAZER_DELAY_DIVIDER;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis(UnityConstants.Axes.Horizontal);
        float moveVertical = Input.GetAxis(UnityConstants.Axes.Vertical);
        
        playerShip.velocity = new Vector3(moveHorizontal,0,moveVertical)* speed;

        float restrictedX = Mathf.Clamp(playerShip.position.x, xMinBorder, xMaxBorder);
        float restrictedZ = Mathf.Clamp(playerShip.position.z, zMinBorder, zMaxBorder);

        playerShip.position = new Vector3(restrictedX, 0 , restrictedZ);

        var velocity = playerShip.velocity;
        playerShip.rotation = Quaternion.Euler(tilt * velocity.z, 0, -velocity.x * tilt);

        if (Time.time > nextShotTime && Input.GetButton(UnityConstants.Axes.Fire1))
        {
            GameObject lazer = Instantiate(lazerShot, lazerGun.position, Quaternion.identity);
            lazer.GetComponent<Rigidbody>().velocity= new Vector3(0,  0, lazerSpeed);
            nextShotTime = Time.time + shotDelay;
        }

        if (Time.time > nextSmallShotTime && Input.GetButton(UnityConstants.Axes.Fire2))
        {
            int isRight = -1;
            foreach (Transform lazer in smallLazers)
            {
                GameObject smallLazer = Instantiate(smallLazerShot, lazer.position, Quaternion.Euler(0, 45, 0));
                smallLazer.transform.rotation = Quaternion.Euler(0, 45, 0);
                smallLazer.GetComponent<Rigidbody>().velocity = new Vector3(lazerSpeed * isRight, 0 ,lazerSpeed);
                smallLazer.transform.localScale /= SMALL_LAZER_DIVIDER;
                isRight = 1;
            }
            nextSmallShotTime = Time.time + smallShotDelay;
            
        }
    }
}
