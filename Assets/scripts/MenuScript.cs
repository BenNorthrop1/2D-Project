using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public GameObject Menu;

    public GameObject Assets;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {













    }


    public void play ()
    {
        SceneManager.LoadScene(1);
    }

    public void assets()
    {
        Menu.SetActive(false);
        Assets.SetActive(true);
    }

    public void back()
    {
        Assets.SetActive(false);
        Menu.SetActive(true);
    }


    public void quit()
    {
        Application.Quit();
    }


}
