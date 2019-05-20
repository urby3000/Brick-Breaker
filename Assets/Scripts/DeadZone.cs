using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    //power ups

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag=="Ball")
        {
            Destroy(collision.gameObject);
            //print(GameObject.FindGameObjectsWithTag("Ball").Length);
            if (GameObject.FindGameObjectsWithTag("Ball").Length==1) {
                GM.instance.LoseLife();
            }
            
        }
    }
}