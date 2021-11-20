using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killfloor : MonoBehaviour
{
    public GameObject Character;
    public Transform respawn_point;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Character.transform.position = respawn_point.transform.position;
        }
    }
}
