using UnityEngine;
using System.Collections;

public class RandomAnim : MonoBehaviour
{
    public int time;
    public Animator anim;
    public int count_anim;

    void Start()
    {
       StartCoroutine(GoAnim());
    }

    IEnumerator GoAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            anim.SetInteger("anim", Random.Range(1, count_anim));
        }
    }

    public void Zero()
    {
        anim.SetInteger("anim", 0);
    }

}
