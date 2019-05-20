using UnityEngine;
using System.Collections;

public class WallOnHit : MonoBehaviour
{
    
    public GameObject blockOnHit;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        Quaternion q = Quaternion.identity;
        Vector3 v = collision.contacts[0].point;
        if (this.name == "Roof")
        {
            q = Quaternion.Euler(new Vector3(0, 0, 90));
            v = v + new Vector3(0, 0.1f, 0);
        }
        else if (this.name == "Wall1")//left
        {
            v = v - new Vector3(0.1f,0,0);
        }
        else//right
        {
            v = v + new Vector3(0.1f, 0, 0);
        }
        Instantiate(blockOnHit, v,q);
    }
}