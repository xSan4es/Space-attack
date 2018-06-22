using UnityEngine;

public class LaserPew : MonoBehaviour
{
    public Vector2 check;
    Rigidbody2D rig;

    void Start()
    {
        rig = transform.parent.GetComponent<Rigidbody2D>();
        check = rig.velocity;
    }
    
    void Update()
    {
        if (check != rig.velocity)
        {
            transform.parent.up = rig.velocity;
            check = rig.velocity;
        }
    }
    

    void OnTriggerEnter2D(Collider2D col)
    {
        // тут відбувається переміщення лазера від одного краю екарну до протилежного
        if (col.gameObject.name == "rightCorner")
        {
            transform.parent.position = new Vector3(SearchVariable.leftCorner.transform.position.x + SearchVariable.leftCorner.transform.localScale.x,
                transform.parent.position.y, transform.parent.position.z);
        }
        if (col.gameObject.name == "leftCorner")
        {
            transform.parent.position = new Vector3(SearchVariable.rightCorner.transform.position.x - SearchVariable.rightCorner.transform.localScale.x,
                 transform.parent.position.y, transform.parent.position.z);
        }
        if (col.gameObject.name == "upCorner")
        {
            transform.parent.position = new Vector3(transform.parent.position.x,
                SearchVariable.downCorner.transform.position.y + SearchVariable.downCorner.transform.localScale.y,
                transform.parent.position.z);
        }
        if (col.gameObject.name == "downCorner")
        {
            transform.parent.position = new Vector3(transform.parent.position.x,
                SearchVariable.upCorner.transform.position.y - SearchVariable.upCorner.transform.localScale.y,
                transform.parent.position.z);
        }
    }
}
    