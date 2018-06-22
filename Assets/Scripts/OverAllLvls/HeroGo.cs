using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HeroGo : MonoBehaviour
{
    public GameObject exp;
    private GameObject child_link;
    private bool shield;
    private CircleCollider2D coll;
    public VirtualJoystickMove joystickMove;
    public VirtualJoystickShoot joystickShoot;
    public static int IsDoubleJoystickController;

    Vector3 moveVec;
    public Transform LaserDied;
    public Text XPLabel;
    private Animator XPanim;
    public Text ScoreLabel;
    public AudioClip shootSound;
    private float playerSpeed = 0.5f; // швидкість руху нашого корабля
    private float playerSpeedMax;
    private float currentSpeed = 0.0f; // Теперішня швидкість
    public static int health = 10;
    private bool tripl_shot;

    Rigidbody2D myBody;

    // біндимо в юніті клавіші для руху
    public Transform laser; // зміна для нашого лазера
    public float laserDistance = 0.2f; // відстань від центру корабля до носа
    public float timeBetweenFires = 0.3f; // час відкату пострілу
    private float timeTilNextFire = 0.0f; // час до наступного пострілу
    public List<KeyCode> shootButton; // клавіша для пострілу
    
    private Vector3 lastMovement = new Vector3(); // збереження останього руху

    public GameObject joystickShootBG;//BackGround

    private Transform myTransform;
    
    void Start()
    {
        //transform.localScale = new Vector3(Screen.width * 0.0001f, Screen.width * 0.0001f, 0);
        //transform.position = new Vector3(0, 0, 0);
        tripl_shot = false;
        myTransform = transform;
        coll = myTransform.GetComponent<CircleCollider2D>();
        child_link = myTransform.GetChild(0).gameObject;
        shield = false;
        child_link.SetActive(false);
        timeBetweenFires = 0.3f;
        playerSpeedMax = 4.0f;
        health = 10;
        XPLabel.text = health.ToString();
        Destroyed_Enemy(0);
        XPanim = XPLabel.GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        Movement(); // ф-я руху

        Shoot();
    }

    void Shoot()
    {
        if (IsDoubleJoystickController == 0)
        {
            joystickShootBG.SetActive(true);
            if (RotationNew() && timeTilNextFire < 0)
            {
                timeTilNextFire = timeBetweenFires;
                if (!tripl_shot) /*TripleShootLaser();*/ShootLaser();
                else if (tripl_shot) TripleShootLaser();
            }
        }
        else
        {
            joystickShootBG.SetActive(false);
            RotationOld(90); // ф-я повороту за мишкою
            foreach (KeyCode element in shootButton) // реалізація стрільби
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (Input.GetKey(element) && timeTilNextFire < 0 && !tripl_shot)
                    {
                        timeTilNextFire = timeBetweenFires;
                        ShootLaser();
                        //TripleShootLaser();
                        break;
                    }
                    else if (Input.GetKey(element) && timeTilNextFire < 0 && tripl_shot)
                    {
                        timeTilNextFire = timeBetweenFires;
                        TripleShootLaser();

                        break;
                    }
                }
            }
        }
        timeTilNextFire -= Time.fixedDeltaTime; // віднімаємо час, який пройшов за проходження одного FixedUpdate
    }

    bool RotationNew()
    {
        Vector3 worldPos = new Vector3(joystickShoot.Horizontal(), joystickShoot.Vertical(), 0);
        if (worldPos == new Vector3(0, 0, 0) || worldPos.magnitude < 0.3f)
        { 
            return false;
        }
        else
        {
            float x = worldPos.x; 
            float y = worldPos.y;
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90; // вираховуємо кут
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));
            myTransform.rotation = rot; // поворот кораблика
            return true;
        }
    }
    void RotationOld(int ang)
    {
        Vector3 worldPos = Input.mousePosition; // беремо позицію мишки
        worldPos = Camera.main.ScreenToWorldPoint(worldPos); // хз нашо це
        float x = myTransform.position.x - worldPos.x; // дістаємо к-ти  
        float y = myTransform.position.y - worldPos.y;
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg; // вираховуємо кут
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + ang));
        myTransform.rotation = rot; // поворот кораблика
    }

    void Movement()
    {
        //Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0);
        
        Vector3 moveVec = new Vector3(joystickMove.Horizontal(), joystickMove.Vertical(), 0);

        if (moveVec.magnitude > 0)
        {
            // рухаємось у вказаному напрямку
            if (currentSpeed < playerSpeedMax)
                currentSpeed += playerSpeed;
            myTransform.Translate(moveVec * currentSpeed * Time.fixedDeltaTime, Space.World);
            lastMovement = moveVec;
            //
        }
        else // якщо нічого не нажали
        {
            //this.transform.Translate(lastMovement  * currentSpeed, Space.World); 
            myTransform.Translate(lastMovement * currentSpeed * Time.fixedDeltaTime, Space.World);
            currentSpeed *= 0.85f;
        }
    }
    
    void ShootLaser()
    {
        // вираховуємо позицію корабля
        float posX = myTransform.position.x + (Mathf.Cos((myTransform.localEulerAngles.z - 90) * Mathf.Deg2Rad) * -laserDistance);
        float posY = myTransform.position.y + (Mathf.Sin((myTransform.localEulerAngles.z - 90) * Mathf.Deg2Rad) * -laserDistance);
        //

        //Instantiate(laser, new Vector2(posX, posY), this.transform.rotation);  //створюємо лазер на носі корабля
        SearchVariable.pool.Launch_prefab(1, new Vector2(posX, posY), myTransform.rotation); //створюємо лазер на носі корабля
        GetComponent<AudioSource>().PlayOneShot(shootSound);
    }

    void TripleShootLaser()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) // якщо входить у ворожий колайдер
    {
        if (col.gameObject.tag.Contains("Died")) // якщо це електрична стінка
        {
            health = 0; // зменшуємо хп
            XPLabel.text = health.ToString();
            //XPanim.
                
        }

        if (col.gameObject.tag.Contains("Laser")) // якщо це лазер
        {
            if (!shield) health -= 1; // зменшуємо хп
            XPLabel.text = health.ToString();
            //k
            
                //GameObject blast = ((Transform)Instantiate(LaserDied, col.transform.position, col.transform.rotation)).gameObject;
                SearchVariable.pool.Launch_prefab(9, myTransform.position, myTransform.rotation);
            
            //Destroy(col.transform.parent.gameObject); // знищуємо лазер
            col.transform.parent.gameObject.SetActive(false);
        }

        if (col.gameObject.tag.Contains("Orb")) 
        {
            health += 3;
            //k
            if (health >= 10) health = 10;
            XPLabel.text = health.ToString();
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.tag.Contains("Rocket"))
        {
            health -= 3;
            //k
            if (health < 0) health = 0;
            XPLabel.text = health.ToString();
            timeTilNextFire += 1;
        }

        Died();
    }

    void OnCollisionEnter2D(Collision2D theCollision) // якщо входить у ворожий колайдер
    {
        if (theCollision.gameObject.tag.Contains("Transport") || theCollision.gameObject.tag.Contains("Enemy")) // якщо це ворог
        {
            if (!shield) health -= 3; // зменшуємо хп
            if (health < 0) health = 0;
            XPLabel.text = health.ToString();
            //k
        }

        Died();
    }

    void Died()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
            MinesGo.Stop();
            TransportGo.Stop();
            AlienGo.Stop();
            SearchVariable.pool.Laser_off();
            /*if (SearchVariable.controller.lvl() >= 3)*/ TurretGo.Stop();
            Instantiate(exp, myTransform.position, myTransform.rotation);
            //SearchVariable.controller.KilledEnemy(); // знищуємо ворога
        }
    }

    public void Destroyed_Enemy(int destroyed_enemy)
    {
        ScoreLabel.text = destroyed_enemy.ToString() + '/' + SearchVariable.controller.Need_to_end().ToString();
    }

    public void Shield_On()
    {
        shield = true;
        child_link.SetActive(true);
        coll.radius *= 1.6f;
    }

    public void Shield_Off()
    {
        shield = false;
        child_link.SetActive(false);
        coll.radius /= 1.6f;
    }

    public float cur_speed()
    {
        return currentSpeed;
    }

    public void cur_speed_set(float sp)
    {
        currentSpeed = sp;
    }

    public void max_speed_set(float sp)
    {
        playerSpeedMax = sp;
    }
}
