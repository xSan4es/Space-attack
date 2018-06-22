using UnityEngine;

public class MinesGo : MonoBehaviour
{
    private bool goHero;
    public static float speed = 1.75f; // швидкість руху ворогів
    private Transform myTransform;
    private Vector3 worldPos;
    private Vector3 delta;

    void Start()
    {
        goHero = false;
        myTransform = transform;
        worldPos = SearchVariable.targetMines.position; // беремо к-ти нашої цілі
        delta = SearchVariable.targetMines.position - myTransform.position;
    }

    void FixedUpdate()
    {
        moveToHero();
    }

    void moveToHero()
    {
        if (goHero == false)
        {
            // рух у напрямку цілі
            delta.Normalize();
            float moveSpeed = speed * Time.fixedDeltaTime;
            myTransform.position = myTransform.position + (delta * moveSpeed);
            //

            Rotation(-90, worldPos); // поворот ворогів в напрямку цілі
        }
        else
        {
            // рух у напрямку цілі
            worldPos = SearchVariable.player.position;
            delta = SearchVariable.player.position - myTransform.position;
            delta.Normalize();
            float moveSpeed = speed * Time.fixedDeltaTime;
            myTransform.position = myTransform.position + (delta * moveSpeed);
            //

            //worldPos = SearchVariable.player.position; // беремо к-ти нашої цілі
            Rotation(-90, worldPos); // поворот ворогів в напрямку цілі
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "MinesPortalIn")
        {
            goHero = true;
            myTransform.position = SearchVariable.controller.placing();
        }
    }

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
        if (goHero)
        {
            worldPos = SearchVariable.targetMines.position; // беремо к-ти нашої цілі
            delta = SearchVariable.targetMines.position - myTransform.position;
            goHero = false;
        }
    }

    public static void Add_Speed()
    {
        speed *= 2;
    }

    public static void Take_Speed()
    {
        speed /= 2;
    }

    public static void Stop()
    {
        speed = 0;
    }
}