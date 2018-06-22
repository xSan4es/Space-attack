using UnityEngine;

public class Random_Box : MonoBehaviour
{
    private int health = 5;
    public AudioClip hitSound;
    public Transform LaserDied;
    //private int check = 0;
    private float curr_time;
    public float lifetime = 10;
    private Transform myTransform;

    void Start()
    {
        health = 5;
        myTransform = transform;
        curr_time = lifetime;
    }

    /*void OnEnable()
    {
        if (check >= 2)
        {
            health = 5;
            curr_time = lifetime;
        }

    }*/

    void OnDisable()
    {
        health = 5;
        curr_time = lifetime;
        //check++;
    }

    void FixedUpdate()
    {
        curr_time -= Time.fixedDeltaTime;
        if (curr_time <= 0)
        {
            myTransform.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col) // якщо входить у ворожий колайдер
    {
        if (col.gameObject.tag.Contains("Hero")) // якщо це герой
        {
            if (myTransform.gameObject.tag == "Box") SearchVariable.variables.Random_Action();
            //health = 0;
            myTransform.gameObject.SetActive(false);
        }

        /*if (col.gameObject.tag.Contains("Laser")) // якщо це лазер
        {
            
            health -= 1; // зменшуємо хп
            //Destroy(col.transform.parent.gameObject); // знищуємо лазер
            col.transform.parent.gameObject.SetActive(false);

            //if (LaserDied)
            //{
            //GameObject blast = ((Transform)Instantiate(LaserDied, myTransform.position, myTransform.rotation)).gameObject;
            SearchVariable.pool.Launch_prefab(9, myTransform.position, myTransform.rotation);
            //}

            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }*/

        //Died();
    }

    void Died()
    {
        if (health <= 0)
        {
            myTransform.gameObject.SetActive(false);
            //Destroy(gameObject);

            /*if (explosion)
            {
                int rand = Random.Range(0, 7);
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
                GameObject heal;
                if (rand == 3)
                {
                    heal = ((Transform)Instantiate(Health, this.transform.position, new Quaternion(0, 0, 0, 0))).gameObject;
                    Destroy(heal, 10f);
                }

                if (rand == 4)
                {
                    heal = ((Transform)Instantiate(Box, this.transform.position, new Quaternion(0, 0, 0, 0))).gameObject;
                    Destroy(heal, 10f);
                }
            }*/
        }
    }
}
