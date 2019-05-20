using UnityEngine;
using System.Collections;

public class HeadScript : MonoBehaviour {

    public Vector3 monsterPos;
    bool traveling = false;
    Vector3 travelTo;
    // Use this for initialization
    public GameObject addScoreT;
    public GameObject projectile;
    float nextAttack;
    float attackDelay = 1f;
    void Start () {
        monsterPos = transform.position;
        nextAttack = Time.time + attackDelay;
	}
	// Update is called once per frame
	void Update () {
        if (!traveling)
        {
           System.Random rnd = new System.Random();
            float randomX = rnd.Next(-14, 15);
            float randomY = rnd.Next(-3, 9);
            travelTo = new Vector3(randomX, randomY, 0);
            traveling = true;
        }
        else {
            transform.position = Vector3.Lerp(transform.position,travelTo,Time.deltaTime*4.2f);
            if (transform.position.x+0.5f >= travelTo.x && transform.position.x - 0.5f <= travelTo.x) {
                traveling = false;
            }
        }
        if (nextAttack <= Time.time) {
            nextAttack = Time.time + attackDelay;
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        // print(collision.collider.gameObject.name);
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            GM.instance.TakeBossLife();
            if (GM.instance.boss)
            {

                TextMesh t = addScoreT.GetComponent<TextMesh>();
                t.text = "+" + 50;
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 50);
                Instantiate(addScoreT, transform.position, Quaternion.identity);
                Destroy(collision.collider.gameObject);
            }
        }
    }
}
