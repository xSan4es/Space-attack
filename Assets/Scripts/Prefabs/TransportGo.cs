using UnityEngine;

public class TransportGo : MonoBehaviour
{
    //private bool goHero = false;

    public static float speed = 1.25f; // швидкість руху ворогів
    
    void FixedUpdate()
    {
        //if (goHero == false)
        //{
            // рух у напрямку цілі
            Vector3 worldPos = SearchVariable.targetTransport.position; // беремо к-ти нашої цілі
            Vector3 delta = SearchVariable.targetTransport.position - transform.position;
            delta.Normalize();
            float moveSpeed = speed * Time.fixedDeltaTime;
            transform.position = transform.position + (delta * moveSpeed);
            Rotation(-90, worldPos); // поворот ворогів в напрямку цілі
        //}
    }

    void Rotation(int ang, Vector3 worldPoss)
    {
        float dx = this.transform.position.x - worldPoss.x; // дістаємо к-ти  
        float dy = this.transform.position.y - worldPoss.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; // вираховуємо кут
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + ang));
        this.transform.rotation = rot; // поворот кораблика
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