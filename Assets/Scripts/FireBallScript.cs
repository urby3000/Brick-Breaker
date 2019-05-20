using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour
{
    Vector3 prevLoc = Vector3.zero;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 curVel = (transform.position - prevLoc) / Time.deltaTime;

        //transform.LookAt(prevLoc);
        if (curVel.y > 0)
        {
            // it's moving up

        }
        else
        {
            // it's moving down
        }
        if (curVel.x > 0)
        {
            // it's moving right
        }
        else
        {
            // it's moving left
        }
        //transform.LookAt(prevLoc);
        //calculate the rotation needed 
        //Quaternion neededRotation = Quaternion.LookRotation(prevLoc - transform.position);

        //use spherical interpollation over time 
        //Quaternion interpolatedRotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * 20);
        prevLoc = transform.position;

    }
}
