using UnityEngine;
using System.Collections;

public class Body3Script : MonoBehaviour {

    // Use this for initialization
    public GameObject addScoreT;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, GameObject.Find("Body2").transform.position, Time.deltaTime * 4.2f);

    }
    void OnCollisionEnter(Collision collision)
    {
        //print(collision.collider.gameObject.name);
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
