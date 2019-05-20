using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public Texture2D cursorTextureNormal;
    public Texture2D cursorTextureClick;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;
    public GameObject ColParticle;

    void Start()
    {
        Cursor.visible = true;
        /*for (int i = 0; i < 10; i++)
        { 
            Debug.Log("Rank: " + PlayerPrefs.GetInt("Rank_" + i) +
                ", Level: " + PlayerPrefs.GetInt("Level_" + i) +
                ", Score: " + PlayerPrefs.GetInt("hiScore_" + i) +
                ", Name: " + PlayerPrefs.GetString("Name_" + i));
        }*/
    }
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTextureNormal, hotSpot, cursorMode);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    // Update is called once per frame
    void Update()
    {
        //Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //Debug.Log(maus);
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                /*if (hit.rigidbody != null)
                    hit.rigidbody.AddForceAtPosition(ray.direction * pokeForce, hit.point);*/
                //Debug.Log(hit.point);
            }
            Cursor.SetCursor(cursorTextureClick, hotSpot, cursorMode);

        }
        else
        {
            Cursor.SetCursor(cursorTextureNormal, hotSpot, cursorMode);
        }

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                /*if (hit.rigidbody != null)
                    hit.rigidbody.AddForceAtPosition(ray.direction * pokeForce, hit.point);*/
                //Debug.Log(hit.point);
            }
            Vector3 maus = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            Instantiate(ColParticle, maus, Quaternion.identity);

        }
    }
}
