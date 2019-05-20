using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{


    public GameObject ColParticle;
    public GameObject addScoreT;
    public GameObject[] powerUps;
    int score = 50;
    int hp = 1;
    void Start()
    {
        if (this.tag == "3HitBrick")
        {
            score = 150;
            hp = 3;

        }
        else if (this.tag == "IndestructibleBrick")
        {
            hp = -1;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].otherCollider.tag == "GreenMonster")
        {

        }
        else if (collision.contacts[0].otherCollider.tag == "PowerUp")
        {

        }
        else if (collision.contacts[0].otherCollider.tag == "Bullet")
        {
            hp--;
            if (hp == 0)
            {


                TextMesh t = addScoreT.GetComponent<TextMesh>();
                t.text = "+" + score;
                Instantiate(addScoreT, transform.position, Quaternion.identity);
                Instantiate(ColParticle, transform.position, Quaternion.identity);

                Destroy(collision.gameObject);

                Destroy(gameObject);
                SpawnPowerUp();

            }
        }
        else if (collision.contacts[0].otherCollider.tag == "Ball")
        {
            if (GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().trough)
            {
                hp = 0;
            }
            else
            {
                hp--;
            }

            if (hp == 0)
            {
                TextMesh t = addScoreT.GetComponent<TextMesh>();
                t.text = "+" + score;
                Instantiate(addScoreT, transform.position, Quaternion.identity);
                Instantiate(ColParticle, transform.position, Quaternion.identity);

                Destroy(gameObject);
                SpawnPowerUp();

            }
        }
    }
    void OnDestroy()
    {
        if (GM.instance != null)
        {
            GM.instance.DestroyBrick();
        }

        if (this.tag == "3HitBrick")
        {

            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 150);
        }
        else if (this.tag == "IndestructibleBrick")
        {

        }
        else
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 50);
        }
    }
    void SpawnPowerUp()
    {

        if (Random.Range(0, 100) <= 20) // probability of x .... 20 %
        {
            Instantiate(powerUps[Random.Range(0, 14)], this.transform.position, new Quaternion());
            //print("WIN");
        }
    }

}
