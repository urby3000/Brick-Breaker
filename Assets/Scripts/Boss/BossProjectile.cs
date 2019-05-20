using UnityEngine;
using System.Collections;

public class BossProjectile : MonoBehaviour
{

    // Use this for initialization
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (GameObject.Find("Paddle(Clone)") != null)
        {
            rb.AddForce((GameObject.Find("Paddle(Clone)").transform.position - transform.position)*25);

        }
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetChild(0).Rotate(0, 0, -140 * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        if (other.name == "Water")
        {
            Destroy(gameObject);
        }
        else if (other.name == "Paddle(Clone)") {
            GM.instance.LoseLife();
        }
    }

}
