using UnityEngine;
using System.Collections;

public class Prefab_Delete : MonoBehaviour
{
    private float curr_time;
    public float lifetime = 2f;
    private Transform myTransform;

    void Start()
    {
        myTransform = transform;
        curr_time = lifetime;
    }

    void OnDisable()
    {
        curr_time = lifetime;
    }

    void Update()
    {
        curr_time -= Time.deltaTime;
        if (curr_time <= 0)
        {
            myTransform.gameObject.SetActive(false);
        }
    }
}
