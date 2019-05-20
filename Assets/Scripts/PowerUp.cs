using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public ParticleSystem effect;
    // Use this for initialization
    void Start () {
        Destroy(gameObject, 9f);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Instantiate(effect, other.gameObject.transform.position, Quaternion.identity);
        }

    }
}
