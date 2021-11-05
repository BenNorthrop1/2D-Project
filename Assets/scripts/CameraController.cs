using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float upOffset = 5;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x , player.transform.position.y+upOffset , -10);
    }
}
