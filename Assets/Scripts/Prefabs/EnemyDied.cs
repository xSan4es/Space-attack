using UnityEngine;

public class EnemyDied : MonoBehaviour
{
    public AudioClip hitSound;
    public int health = 2; // хп ворогів
    private Transform myTransform;
    private int check = 0;
    
    void Start()
    {
        myTransform = transform;
        
    }

    void OnCollisionEnter2D(Collision2D theCollision) // якщо входить у ворожий колайдер
    {

        if (theCollision.gameObject.name.Contains("Hero")) // якщо це герой
        {
            health = 0; // зменшуємо хп
        }
        
        Died();
    }

    void OnTriggerEnter2D(Collider2D col) // якщо входить у ворожий колайдер
    {
        if (col.gameObject.tag.Contains("Laser")) // якщо це лазер
        {
            health -= 1; // зменшуємо хп
            //Destroy(col.transform.parent.gameObject); // знищуємо лазер
            col.transform.parent.gameObject.SetActive(false);
            
            //GameObject blast = ((Transform)Instantiate(LaserDied, myTransform.position, myTransform.rotation)).gameObject;
            SearchVariable.pool.Launch_prefab(9, myTransform.position, myTransform.rotation);
            
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }

        if (col.gameObject.tag.Contains("Rocket") || col.gameObject.tag.Contains("Died")) // якщо це лазер
        {
            health = 0;
        }

        Died();
    }

    void Died()
    {
        if (health <= 0)
        {
            int killed = PlayerPrefs.GetInt("killedEnemy");
            killed++;
            PlayerPrefs.SetInt("killedEnemy", killed);
            //Destroy(this.gameObject);
            myTransform.gameObject.SetActive(false);

            //SearchVariable.controller.KilledEnemy(); // знищуємо ворога
            
                int rand = Random.Range(0, 5);
            //GameObject exploder = ((Transform)Instantiate(explosion, myTransform.position, myTransform.rotation)).gameObject;
            SearchVariable.pool.Launch_prefab(8, myTransform.position, myTransform.rotation);
            
            if (rand == 3)
            {
                //heal = ((Transform)Instantiate(Health, myTransform.position, new Quaternion(0, 0, 0, 0))).gameObject;
                SearchVariable.pool.Launch_prefab(6, myTransform.position, new Quaternion(0, 0, 0, 0));
            }

            //rand = Random.Range(0, 3);

            if (rand == 2 && SearchVariable.controller.lvl() != 1)
            {
                //heal = ((Transform)Instantiate(Box, myTransform.position, new Quaternion(0, 0, 0, 0))).gameObject;
                SearchVariable.pool.Launch_prefab(7, myTransform.position, new Quaternion(0, 0, 0, 0));
            }

            if (myTransform.gameObject.tag == "Transport")
            {
                SearchVariable.destroyed_enemy++;
                SearchVariable.heroGo.Destroyed_Enemy(SearchVariable.destroyed_enemy);
                SearchVariable.all_enemy_dest++;

                if (SearchVariable.all_enemy_dest == SearchVariable.controller.Need_to_end())
                {
                    SearchVariable.controller.end_game();
                }
            }

            PlayerPrefs.Save();
        }
    }

    void OnEnable()
    {
        if (check >= 2)
        {
            if (myTransform.gameObject.tag == "Transport") health = 1;
            else health = 3;
        }
    }

    void OnDisable()
    {
        check++;
    }
}
