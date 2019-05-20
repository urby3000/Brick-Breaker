using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paddle : MonoBehaviour
{
    private Vector3 playerPos = new Vector3(0, -8.5f, 0);
    float speed;
    float low_speed = 0.35f;
    float med_speed = 0.55f;
    float high_speed = 0.75f;

    //power up vars
    public GameObject hearty;
    public float paddle_large = 0f;
    public bool auto = false;
    public float ballInitialVelocity = 400f;
    public float invert_control = 1;
    public GameObject bal;
    public GameObject darky;
    public bool laser = false;
    public GameObject weaponleft;
    public GameObject weaponright;
    public GameObject projectile;
    public float nextAttack;
    float attackDelay = 0.6f;

    //power up bools

    void Start()
    {
        ResetPowerUps();
        if (GM.instance.boss)
        {
            nextAttack = Time.time;
            laser = true;
            Instantiate(weaponleft);
            GameObject.Find("WeaponLeft(Clone)").transform.parent = transform;
            GameObject.Find("WeaponLeft(Clone)").transform.position = transform.position + new Vector3(-1, 0.516f, -0.631f);
            Instantiate(weaponright);
            GameObject.Find("WeaponRight(Clone)").transform.parent = transform;
            GameObject.Find("WeaponRight(Clone)").transform.position = transform.position + new Vector3(1, 0.516f, -0.631f);
        }
        Cursor.visible = false;
        //Debug.Log(PlayerPrefs.GetInt("MouseSpeed"));
        if (PlayerPrefs.GetInt("MouseSpeed") == 0)
        {
            speed = low_speed;
        }
        else if (PlayerPrefs.GetInt("MouseSpeed") == 1)
        {
            speed = med_speed;
        }
        else
        {
            speed = high_speed;
        }
    }



    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (auto == false)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    float xPos = invert_control * -0.2f;
                    playerPos.x = Mathf.Clamp(playerPos.x + xPos, -14f + paddle_large, 14f - paddle_large);
                    transform.position = playerPos;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    float xPos = invert_control * 0.2f;
                    playerPos.x = Mathf.Clamp(playerPos.x + xPos, -14f + paddle_large, 14f - paddle_large);
                    transform.position = playerPos;
                }
                if (Input.GetAxis("Mouse X") < 0)
                {
                    //print("Mouse moved left");
                    playerPos.x = Mathf.Clamp(playerPos.x - invert_control * speed, -14f + paddle_large, 14f - paddle_large);
                    transform.position = playerPos;

                }
                if (Input.GetAxis("Mouse X") > 0)
                {

                    //print("Mouse moved right");
                    playerPos.x = Mathf.Clamp(playerPos.x + invert_control * speed, -14f + paddle_large, 14f - paddle_large);
                    transform.position = playerPos;
                }
            }
            else
            {
                if (GameObject.Find("Ball") != null)
                {
                    this.transform.position = new Vector3(GameObject.Find("Ball").transform.position.x, this.transform.position.y, 0);
                }
            }
            if (laser)
            {
                if ((Input.GetMouseButtonDown(0) || Input.GetKey("space")))
                {
                    if (Time.time >= nextAttack)
                    {
                        nextAttack = Time.time + attackDelay;

                        GameObject bul1 = (GameObject)Instantiate(projectile, GameObject.Find("WeaponLeft(Clone)").transform.position, new Quaternion());

                        GameObject bul2 = (GameObject)Instantiate(projectile, GameObject.Find("WeaponRight(Clone)").transform.position, new Quaternion());

                        Vector3 a = new Vector3(0, 800, 10);
                        bul1.GetComponent<Rigidbody>().AddForce(a);
                        bul2.GetComponent<Rigidbody>().AddForce(a);


                    }
                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            if (other.gameObject.name == "APowerUp(Clone)")// auto bounce
            {
                ResetPowerUps();
                auto = true;
            }
            else if (other.gameObject.name == "EPowerUp(Clone)") //enlarge
            {
                Transform[] children = new Transform[this.gameObject.transform.childCount];
                int i = 0;
                foreach (Transform T in transform)
                {
                    children[i++] = T;
                }
                gameObject.transform.DetachChildren();
                this.gameObject.transform.localScale = new Vector3(5, 1, 1);  // Scale        
                foreach (Transform T in children) // Re-Attach
                {
                    T.parent = transform;

                }
                paddle_large = 1f;

            }
            else if (other.gameObject.name == "FPowerUp(Clone)")//faster ball
            {
                ballInitialVelocity = 600f;
                GameObject.Find("Ball").GetComponent<Ball>().ballInitialVelocity = ballInitialVelocity;
            }
            else if (other.gameObject.name == "IPowerUp(Clone)")// inverta controle
            {
                ResetPowerUps();
                invert_control = -1;
            }
            else if (other.gameObject.name == "LPowerUp(Clone)")// laser
            {
                ResetPowerUps();
                nextAttack = Time.time;
                laser = true;
                Instantiate(weaponleft);
                GameObject.Find("WeaponLeft(Clone)").transform.parent = transform;
                GameObject.Find("WeaponLeft(Clone)").transform.position = transform.position + new Vector3(-1, 0.516f, -0.631f);
                Instantiate(weaponright);
                GameObject.Find("WeaponRight(Clone)").transform.parent = transform;
                GameObject.Find("WeaponRight(Clone)").transform.position = transform.position + new Vector3(1, 0.516f, -0.631f);

            }
            else if (other.gameObject.name == "MPowerUp(Clone)") // potroji žoge
            {
                Instantiate(bal, GameObject.Find("Ball").transform.position, new Quaternion());
                Instantiate(bal, GameObject.Find("Ball").transform.position, new Quaternion());
                float change_x = -1;
                foreach (GameObject zoga in GameObject.FindGameObjectsWithTag("Ball"))
                {
                    if (zoga.name == "Ball(Clone)")
                    {
                        zoga.GetComponent<Ball>().prev = GameObject.Find("Ball").GetComponent<Ball>().prev;
                        zoga.GetComponent<Ball>().rb.isKinematic = false;
                        zoga.GetComponent<Ball>().prev = new Vector3(zoga.GetComponent<Ball>().prev.x * change_x, zoga.GetComponent<Ball>().prev.y, 0);
                        change_x = 0;
                        zoga.GetComponent<Ball>().rb.AddForce(zoga.GetComponent<Ball>().prev);
                        zoga.name = "Ball";
                        //print(zoga.GetComponent<Ball>().prev);
                    }
                }

            }
            else if (other.gameObject.name == "NPowerUp(Clone)")// ladja nevidna
            {
                ResetPowerUps();
                Transform[] children = new Transform[this.gameObject.transform.childCount];
                int i = 0;
                foreach (Transform T in transform)
                {
                    children[i++] = T;
                }
                gameObject.transform.DetachChildren();

                Color c = gameObject.GetComponent<Renderer>().material.color;
                c.a = 0.05f;
                this.gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, c.a);
                foreach (Transform T in children) // Re-Attach
                {
                    T.parent = transform;

                }
            }
            else if (other.gameObject.name == "RPowerUp(Clone)")// ladja se pomanjša
            {
                Transform[] children = new Transform[this.gameObject.transform.childCount];
                int i = 0;
                foreach (Transform T in transform)
                {
                    children[i++] = T;
                }
                gameObject.transform.DetachChildren();
                this.gameObject.transform.localScale = new Vector3(2, 1, 1);  // Scale        
                foreach (Transform T in children) // Re-Attach
                {
                    T.parent = transform;

                }
                paddle_large = -1f;

            }
            else if (other.gameObject.name == "SPowerUp(Clone)")// žoga se upočasni
            {
                ballInitialVelocity = 250f;
                GameObject.Find("Ball").GetComponent<Ball>().ballInitialVelocity = ballInitialVelocity;
            }
            else if (other.gameObject.name == "QPowerUp(Clone)")// žoga se ob dotiku ladje prilepi
            {
                ResetPowerUps();
                GameObject.Find("Ball").GetComponent<Ball>().stick = true;
            }
            else if (other.gameObject.name == "WPowerUp(Clone)")// potemni zaslon
            {
                ResetPowerUps();
                Instantiate(darky);
            }
            else if (other.gameObject.name == "XPowerUp(Clone)")// če se dotakne kocke jo samo uniči in gre naprej
            {
                ResetPowerUps();
                GameObject.Find("Ball").GetComponent<Ball>().trough = true;
            }
            else if (other.gameObject.name == "ZPowerUp(Clone)")// next lvl
            {
                GM.instance.index = 4;// uporabu sm tisto kodo ko pa za cheatcode ...
            }
            else if (other.gameObject.name == "PPowerUp(Clone)")// add life
            {
                GM.instance.lives = GM.instance.lives + 1;
                GameObject hearts = GameObject.Find("Hearts(Clone)");
                List<GameObject> children = new List<GameObject>();
                foreach (Transform child in hearts.transform)
                {
                    children.Add(child.gameObject);
                }
                Vector3 v;
                if (children.Count - 1==-1) {
                    v = new Vector3(-14.81f, -11.84f, -2.11f);
                } else {
                    v = children[children.Count - 1].transform.position;
                }
                GameObject g=(GameObject)Instantiate(hearty, v+new Vector3(2,0,0), Quaternion.identity);
                g.transform.parent = hearts.transform;
            }
            Destroy(other.gameObject);
        }

    }
    void ResetPowerUps()
    {
        paddle_large = 0f;

        auto = false;

        invert_control = 1;

        laser = false;
        if (GameObject.Find("WeaponLeft(Clone)"))
        {
            Destroy(GameObject.Find("WeaponLeft(Clone)"));
            Destroy(GameObject.Find("WeaponRight(Clone)"));

        }

        Transform[] children = new Transform[this.gameObject.transform.childCount];
        int i = 0;
        foreach (Transform T in transform)
        {
            children[i++] = T;
        }
        gameObject.transform.DetachChildren();

        Color c = gameObject.GetComponent<Renderer>().material.color;
        c.a = 1f;
        this.gameObject.transform.localScale = new Vector3(3, 1, 1);  // Scale 
        this.gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, c.a);
        foreach (Transform T in children) // Re-Attach
        {
            T.parent = transform;

        }
        if (
        GameObject.Find("Ball").GetComponent<Ball>().stick)
        {

            GameObject.Find("Ball").GetComponent<Ball>().stick = false;

            GameObject.Find("Ball").GetComponent<Ball>().rb.isKinematic = false;

        }
        if (GameObject.Find("Darken(Clone)") != null)
        {
            Destroy(GameObject.Find("Darken(Clone)"));
        }

        GameObject.Find("Ball").GetComponent<Ball>().trough = false;
    }

}