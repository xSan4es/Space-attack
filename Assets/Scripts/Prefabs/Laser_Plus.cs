using UnityEngine;
using System.Collections;

public class Laser_Plus : MonoBehaviour
{
    public float speed = 7.0f; // швидкість польоту лазера
    public float lifetime = 2.0f; // тривалість життя лазера
    public float curr_time;
    Rigidbody2D rig;
    private int check = 0;

    private Transform myTransform;

    void Start()
    {
        myTransform = transform;
        curr_time = lifetime;
        rig = GetComponent<Rigidbody2D>();
        rig.AddRelativeForce(new Vector2(0, 1) * speed / 100f);
        //Destroy(gameObject, lifetime); // знищує лазер через вказаний час
    }

    void FixedUpdate()
    {
        curr_time -= Time.fixedDeltaTime;
        if (curr_time <= 0)
        {
            myTransform.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        if (check >= 2)
        {
            rig.AddRelativeForce(new Vector2(0, 1) * speed / 100f);
            curr_time = lifetime;
        }

    }

    void OnDisable()
    {
        check++;
    }
}

