using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazerScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(UnityConstants.Tags.Player))
        {
            other.GetComponent<PlayerScript>().DestroyPlayer();
        }
    }
}
