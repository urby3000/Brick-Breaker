using UnityEngine;
using System.Collections;

public class SparkleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Paddle(Clone)") != null)
        {
            this.transform.position = GameObject.Find("Paddle(Clone)").transform.position;
        }
        else {
            Destroy(gameObject);
        }
	}
}
