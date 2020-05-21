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

    void Start()
    {
        playerShip = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        playerShip.velocity = new Vector3(moveHorizontal,0,moveVertical)* speed;

        float restrictedX = Mathf.Clamp(playerShip.position.x, xMinBorder, xMaxBorder);
        float restrictedZ = Mathf.Clamp(playerShip.position.z, zMinBorder, zMaxBorder);

        playerShip.position = new Vector3(restrictedX, 0 , restrictedZ);

        var velocity = playerShip.velocity;
        playerShip.rotation = Quaternion.Euler(tilt * velocity.z, 0, -velocity.x * tilt);
    }
}
