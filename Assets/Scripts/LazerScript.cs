using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    [SerializeField] private float speed;
    void Start()
    {
        GetComponent<Rigidbody>().velocity= new Vector3(0,  0, speed);
    }
}
