using UnityEngine;
using System.Collections;

public class Corners : MonoBehaviour
{
    public new Camera camera;

    public BoxCollider2D upCorner;
    public BoxCollider2D downCorner;
    public BoxCollider2D leftCorner;
    public BoxCollider2D rightCorner;
    private float r_1;


    // Use this for initialization
    void Start()
    {
        setCorners();
    }
    void setCorners()
    {
        //upCorner.size = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x, 0.1f);
        upCorner.transform.position = new Vector3(0, camera.ScreenToWorldPoint(new Vector3(0, Screen.height * 1.02f, 0)).y, 0);
        
        r_1 = (Screen.height * 1.02f) - Screen.height;

        //downCorner.size = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x, 0.1f);
        downCorner.transform.position = new Vector3(0, camera.ScreenToWorldPoint(new Vector3(0,-r_1, 0)).y, 0);
        //downCorner.transform.position = new Vector3(0, camera.ScreenToWorldPoint(new Vector3(0, -(Screen.height * 1.02f * 0.5f), 0)).y, 0);

        //rightCorner.size = new Vector2(0.1f, camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y);
        rightCorner.transform.position = new Vector3(camera.ScreenToWorldPoint(new Vector3( Screen.width * 1.01f, 0, 0)).x, 0, 0);

        r_1 = (Screen.width * 1.01f) - Screen.width;

        //leftCorner.size = new Vector2(0.1f, camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y);
        leftCorner.transform.position = new Vector3(camera.ScreenToWorldPoint(new Vector3(-r_1, 0, 0)).x, 0, 0);
        //leftCorner.transform.position = new Vector3(camera.ScreenToWorldPoint(new Vector3(-(Screen.width * 1.01f * 0.5f), 0, 0)).x, 0, 0);
    }

}
