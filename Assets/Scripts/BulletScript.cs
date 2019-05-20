using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Paddle(Clone)")
        {

        }
        else
        {
            Destroy(gameObject,0.1f);
        }
    }
}
