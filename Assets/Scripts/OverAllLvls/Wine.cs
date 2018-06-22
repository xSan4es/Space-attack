using UnityEngine;

public class Wine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hero")
        {
            col.gameObject.SetActive(false);
            MinesGo.Stop();
            TransportGo.Stop();
            AlienGo.Stop();
            SearchVariable.pool.Laser_off();
            /*if (SearchVariable.controller.lvl() >= 3)*/
            TurretGo.Stop();
            Lose.WIN = true;
        }
    }
}
