using UnityEngine;
using System.Collections;

public class DestroyFireParticle : MonoBehaviour {

    // Use this for initialization
    Vector3 prevLoc = Vector3.zero;
    void Start () {
        //Destroy(gameObject, GetComponent<ParticleSystem>().duration);
    }
	
	// Update is called once per frame
	void Update () {

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
        else {
            // it's moving left
        }
        //transform.LookAt(prevLoc);
        prevLoc = transform.position;
    }
}
