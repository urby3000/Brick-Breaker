using UnityEngine;
using System.Collections;

public class TextAddScoreScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -10, -3), 1f * Time.deltaTime);
        gameObject.GetComponent<Renderer>().material.color =
               new Color(gameObject.GetComponent<Renderer>().material.color.r,
               gameObject.GetComponent<Renderer>().material.color.g,
               gameObject.GetComponent<Renderer>().material.color.b,
               Mathf.Clamp(gameObject.GetComponent<Renderer>().material.color.a - 0.02f, 0f,1f));
    }
}
