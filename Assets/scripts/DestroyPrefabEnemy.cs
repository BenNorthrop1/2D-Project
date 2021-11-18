using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefabEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1.00f);


    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collider.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }
}
