using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Helper;

public class coin : MonoBehaviour
{


    public TMPro.TextMeshProUGUI CoinsUI;





    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Destroy(gameObject);
            coins++;
            CoinsUI.text = "x " + coins.ToString();
        }
    }







}
