using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class Helper : MonoBehaviour
{
    public static float playerHealth;


    public static void DoFaceLeft(GameObject obj, bool faceleft)
    {
        if (faceleft == true)
        {
            obj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public static int GetObjectDir(GameObject obj)
    {
        float ang = obj.transform.eulerAngles.y;
        if (ang == 180)
        {
            return Left;
        }
        else
        {
            return Right;
        }
    }

    public static void MakeBullet(GameObject prefab, float xpos, float ypos, float xvel, float yvel)
    {
        // instantiate the object at xpos,ypos
        GameObject instance = Instantiate(prefab, new Vector3(xpos, ypos, 0), Quaternion.identity);

        // set the velocity of the instantiated object
        SetVelocity(instance, xvel, yvel);

        // set the direction of the instance based on the x velocity
        DoFaceLeft(instance, xvel < 0 ? true : false);
    }

    public static void SetVelocity(GameObject obj, float xvel, float yvel)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(xvel, yvel, 0);


    }
}