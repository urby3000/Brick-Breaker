using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

    public float ballInitialVelocity = 400f;
    public GameObject ball;
    public GameObject FireParticle;
    public GameObject ColParticle;
    int i = 0; // učasih se zatakne
    public Vector3 prev;
    public Rigidbody rb;
    private bool ballInPlay;

    //powerups
    //go trough
    public bool trough = false;
    //sticky
    public bool stick = false;


    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }
    void Start()
    {
        if (this.name == "Ball(Clone)") {
           /* prev = GameObject.Find("Ball").GetComponent<Ball>().prev;
            rb.isKinematic = false;
            rb.AddForce(prev);*/
        }
    }
    void FixedUpdate()
    {
        if (rb.velocity.magnitude < 0.01 && ballInPlay)//no velocity ...
        {
           
            i++;
            if (i > 1) {
                rb.AddForce(new Vector3(ballInitialVelocity,ballInitialVelocity,0));
            }
        }
    }

    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (!GM.instance.boss)
            {
                if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) && ballInPlay == false)
                {
                    transform.parent = null;
                    ballInPlay = true;
                    rb.isKinematic = false;
                    rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
                    prev = new Vector3(ballInitialVelocity, ballInitialVelocity, 0);
                }
                if (stick)
                {
                    //print("sticky");
                    if (rb.isKinematic == true)
                    {
                        transform.position = GameObject.Find("Paddle(Clone)").transform.position + new Vector3(0, 1.5f, 0);
                    }
                    if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")))
                    {
                        rb.isKinematic = false;
                        // rb.AddForce(prev);
                    }
                }
                Vector3 pos = transform.position;
                pos.z = 0;
                ball.transform.position = pos;
            }
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        i=0;
        Vector3 relativePosition = transform.InverseTransformPoint(collision.contacts[0].point);
        //Transform pos= collision.transform;
        //Debug.Log(pos);
        Instantiate(ColParticle, collision.contacts[0].point, Quaternion.identity);
        float x, y;
        rb.velocity = Vector3.zero;
        if (relativePosition.x > 0)
        {
           // print("The object is to the right");
            x = -ballInitialVelocity;
        }
        else
        {
            //print("The object is to the left");
            x = ballInitialVelocity;
        }

        if (relativePosition.y > 0)
        {
           // print("The object is above.");
            y = -ballInitialVelocity;
        }
        else
        {
           // print("The object is below.");
            y = ballInitialVelocity;
        }

        //print(collision.collider.name);
        if (collision.collider.name == "Wall1" || collision.collider.name == "Wall2" || collision.collider.name == "Roof" || collision.collider.name == "Paddle(Clone)")
        {
           // print("normal");
            prev = new Vector3(x, y, 0);
            rb.AddForce(new Vector3(x, y, 0));
        }
        else
        {
            if (trough)
            {
                 //print(collision.collider.tag);
                if (collision.collider.tag == "IndestructibleBrick")
                {
                    Destroy(collision.collider.gameObject);
                }
                rb.AddForce(prev);
            }
            else
            {
               // print("normal");
                prev = new Vector3(x, y, 0);
                rb.AddForce(new Vector3(x, y, 0));
            }
        }
        if (stick && collision.collider.name == "Paddle(Clone)") {
            rb.isKinematic = true;
        }
    }
}