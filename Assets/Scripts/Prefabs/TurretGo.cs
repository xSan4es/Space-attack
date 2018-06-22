using UnityEngine;

public class TurretGo : MonoBehaviour
{

    public Transform laser; // зміна для нашого лазера
    private float laserDistance = 0.4f; // відстань від центру корабля до носа
    public static float timeBetweenFires = 2f; // час відкату пострілу
    private float timeTilNextFire = 1f; // час до наступного пострілу
    public AudioClip shootSound;
    private Transform myTransform;
    Vector3 worldPos;
    public static bool check;

    void Start()
    {
        myTransform = transform;
        timeBetweenFires = 2f;
        check = false;
    }

    void FixedUpdate()
    {
        worldPos = SearchVariable.player.position; // беремо к-ти нашої цілі
        Rotation(90, worldPos); // поворот ворогів в напрямку цілі

        if (timeTilNextFire < 0 && !check)
        {
            timeTilNextFire = timeBetweenFires;
            ShootLaser();
        }
        timeTilNextFire -= Time.fixedDeltaTime;
    }
    
    void Rotation(int ang, Vector3 worldPoss)
    {
        float dx = this.transform.position.x - worldPoss.x; // дістаємо к-ти  
        float dy = this.transform.position.y - worldPoss.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; // вираховуємо кут
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + ang));
        this.transform.rotation = rot; // поворот кораблика
    }

    void ShootLaser()
    {
        // вираховуємо позицію корабля
        float posX = this.transform.position.x + (Mathf.Cos((transform.localEulerAngles.z - 90) * Mathf.Deg2Rad) * -laserDistance);
        float posY = this.transform.position.y + (Mathf.Sin((transform.localEulerAngles.z - 90) * Mathf.Deg2Rad) * -laserDistance);
        //

        SearchVariable.pool.Launch_prefab(2, new Vector2(posX, posY), myTransform.rotation);  //створюємо лазер на носі корабля
        GetComponent<AudioSource>().PlayOneShot(shootSound);
    }

    public static void Add_Speed()
    {
        timeBetweenFires = 1;
    }

    public static void Take_Speed()
    {
        timeBetweenFires = 2;
    }

    public static void Stop()
    {
        check = true;
    }
}