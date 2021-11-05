using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coin : MonoBehaviour
{


    public TMPro.TextMeshProUGUI CoinsUI;

    public int coins = 0;



    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            Destroy(gameObject);
            coins++;
            CoinsUI.text = "x " + coins.ToString();
        }
    }







}
