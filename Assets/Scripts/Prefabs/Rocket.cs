using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    public static float speed = 3.5f; // швидкість руху ворогів
    private Transform myTransform;
    private Vector3 worldPos;
    private Vector3 delta;
    private int health;
    private int check = 0;
    public Transform expos;

    void Start()
    {
        health = 2;
        myTransform = transform;
        //expos = myTransform.GetChild(0).gameObject;
        //expos.SetActive(false);
        worldPos = SearchVariable.targetMines.position; // беремо к-ти нашої цілі
        delta = SearchVariable.targetMines.position - myTransform.position;
    }

    void FixedUpdate()
    {
        moveToHero();
    }

    void moveToHero()
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

    void Rotation(int ang, Vector3 worldPoss) //
    {
        float dx = myTransform.position.x - worldPoss.x; // дістаємо к-ти  
        float dy = myTransform.position.y - worldPoss.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; // вираховуємо кут
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + ang));
        myTransform.rotation = rot; // поворот кораблика
    }
    
    void OnTriggerEnter2D(Collider2D col) // якщо входить у ворожий колайдер
    {
        if (col.gameObject.tag.Contains("Laser")) // якщо це лазер
        {
            health -= 1; // зменшуємо хп
            //Destroy(col.transform.parent.gameObject); // знищуємо лазер
            col.transform.parent.gameObject.SetActive(false);
            if (health <= 0)
            {
                //expos.SetActive(true);
                Transform expos_ = Instantiate(expos, myTransform.position, myTransform.rotation);
                Destroy(expos_, 1);
                expos_.SetParent(SearchVariable.new_parent);
                myTransform.gameObject.SetActive(false);
            }
        }

        if (col.gameObject.tag.Contains("Transport") || col.gameObject.tag.Contains("Enemy") || col.gameObject.tag.Contains("Hero")) // якщо це ворог
        {
            //expos.SetActive(true);
            Transform expos_ = Instantiate(expos, myTransform.position, myTransform.rotation);
            Destroy(expos_, 1);
            expos_.SetParent(SearchVariable.new_parent);
            myTransform.gameObject.SetActive(false);
        }
    }

    /*void OnCollisionEnter2D(Collision2D theCollision) // якщо входить у ворожий колайдер
    {
        if (theCollision.gameObject.tag.Contains("Transport") || theCollision.gameObject.tag.Contains("Enemy") || theCollision.gameObject.tag.Contains("Hero")) // якщо це ворог
        {
            myTransform.gameObject.SetActive(false);
            expos.SetActive(true);
        }
    }*/

    void OnEnable()
    {
        if (check >= 2)
        {
           health = 3;
        }
    }

    void OnDisable()
    {
        check++;
    }
}
