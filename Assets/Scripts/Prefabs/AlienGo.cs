using UnityEngine;
using System.Collections;

public class AlienGo : MonoBehaviour
{
    private Transform child;
    public static float speed = 0.75f; // швидкість руху ворогів
    private Transform myTransform;
    private Vector3 worldPos;

    IEnumerator rocketGo()
    {
        yield return new WaitForSeconds(Random.Range(0,3) + 3);
        child.GetComponent<Animator>().enabled = true;
        child.GetComponent<Rocket>().enabled = true;
        child.GetComponent<BoxCollider2D>().enabled = true;
        child.SetParent(SearchVariable.new_parent);
        yield return new WaitForSeconds(4);
        myTransform.gameObject.SetActive(false);
        yield break;
    }

    void Start()
    {
        myTransform = transform;
        child = myTransform.GetChild(0);
        speed = 0.75f;
    }

    void FixedUpdate()
    {
        worldPos = SearchVariable.player.position;
        moveToHero();
    }

    void moveToHero()
    {
        float moveSpeed = speed * Time.fixedDeltaTime;
        float x = Mathf.Cos(1) * 3;
        float y = Mathf.Sin(1) * 1.3f;
        myTransform.position = myTransform.position + (new Vector3 (x,y,0) * moveSpeed);
        
        Rotation(-90, worldPos); // поворот ворогів в напрямку цілі
    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "MinesPortalIn")
        {
            goHero = true;
            myTransform.position = SearchVariable.controller.placing();
        }
    }*/

    void Rotation(int ang, Vector3 worldPoss) //
    {
        float dx = myTransform.position.x - worldPoss.x; // дістаємо к-ти  
        float dy = myTransform.position.y - worldPoss.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; // вираховуємо кут
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + ang));
        myTransform.rotation = rot; // поворот кораблика
    }

    void OnEnable()
    {
        StartCoroutine(rocketGo());
    }

    /*public static void Add_Speed()
    {
        speed *= 2;
    }

    public static void Take_Speed()
    {
        speed /= 2;
    }*/

    public static void Stop()
    {
        speed = 0;
    }
}