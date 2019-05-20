using UnityEngine;
using System.Collections;

public class Bricks : MonoBehaviour
{

    public GameObject brickParticle;
    public GameObject ColParticle;

    void OnCollisionEnter(Collision other)
    {
        Instantiate(ColParticle, transform.position, Quaternion.identity);
        GM.instance.DestroyBrick();
        Destroy(gameObject);
    }
}