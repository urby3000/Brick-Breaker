using UnityEngine;
using System.Collections;

public class GreenMonsterScript : MonoBehaviour
{

    public float monsterInitialVelocity = 1f;
    public GameObject addScoreT;
    Vector3 prev;
    public GameObject ColParticle;
    public GameObject DestroyParticle;
    int i = 0; // učasih se zatakne
    private Rigidbody rb;
    // Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }
    void Start()
    {
        
        rb.isKinematic = false;
        rb.AddForce(new Vector3(monsterInitialVelocity, -monsterInitialVelocity, 0));
    }
    void FixedUpdate()
    {
        if (rb.velocity.magnitude < 0.01)//no velocity ...
        {
            //Debug.Log(i.ToString() +" unity answers saves the day!");
            i++;
            if (i > 2)
            {
                rb.AddForce(prev);
            }
        }

        if (transform.position.y < -5.2f)
        {

            rb.AddForce(new Vector3(prev.x, monsterInitialVelocity, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            this.gameObject.transform.GetChild(0).Rotate(0, -40 * Time.deltaTime, 0, Space.World);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].otherCollider.tag == "Ball")
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 200);

            TextMesh t = addScoreT.GetComponent<TextMesh>();
            t.text= "+200";
            Instantiate(addScoreT, transform.position, Quaternion.identity);
            Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else {

            i = 0;
            Vector3 relativePosition = transform.InverseTransformPoint(collision.contacts[0].point);
            //Transform pos= collision.transform;
            //Debug.Log(pos);
            Instantiate(ColParticle, collision.contacts[0].point, Quaternion.identity);
            float x, y;
            rb.velocity = Vector3.zero;
            if (relativePosition.x > 0)
            {
                // print("The object is to the right");
                x = -monsterInitialVelocity;
            }
            else
            {
                //print("The object is to the left");
                x = monsterInitialVelocity;
            }

            if (relativePosition.y > 0)
            {
                // print("The object is above.");
                y = -monsterInitialVelocity;
            }
            else
            {
                // print("The object is below.");
                y = monsterInitialVelocity;
            }
            prev = new Vector3(x, y, 0);
            rb.AddForce(new Vector3(x, y, 0));
        }
    }
}
