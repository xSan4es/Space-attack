using UnityEngine;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    public Transform objectsParent;
    public List<GameObject> All_objects = new List<GameObject>();
    private List<Transform> All_objects_cache = new List<Transform>();
    private List<AudioSource> audio_cache = new List<AudioSource>();
    public GameObject blue_Lazer;
    public GameObject red_Lazer;
    public GameObject mine;
    public GameObject transport;
    public GameObject turrel;
    public GameObject orb;
    public GameObject box;
    public GameObject explosion;
    public GameObject laserDied;
    public GameObject teleport;

    private int from;
    private int to;

    private int sum_undo_blue_Lazer;
    public int count_blue_Lazer;

    private int sum_undo_red_Lazer;
    public int count_red_Lazer;

    private int sum_undo_mine;
    public int count_mine;

    private int sum_undo_transport;
    public int count_transport;

    private int sum_undo_turrel;
    public int count_turrel;

    private int sum_undo_orb;
    public int count_orb;

    private int sum_undo_box;
    public int count_box;

    private int sum_undo_explosion;
    public int count_explosion;

    private int sum_undo_laserDied;
    public int count_laserDied;

    private int sum_undo_teleport;
    public int count_teleport;

    private int sum_on_audio;
    public int count_objects;

    void Awake()
    {
        count_objects = count_blue_Lazer + count_red_Lazer + count_mine + count_transport + count_turrel + count_orb + count_box + count_explosion + count_laserDied + count_teleport;

        sum_undo_blue_Lazer = 0;
        sum_undo_red_Lazer = count_blue_Lazer + sum_undo_blue_Lazer;
        sum_undo_mine = count_red_Lazer + count_blue_Lazer;
        sum_undo_transport = count_mine + sum_undo_mine;
        sum_undo_turrel = count_transport + sum_undo_transport;
        sum_undo_orb = count_turrel + sum_undo_turrel;
        sum_undo_box = count_orb + sum_undo_orb;
        sum_undo_explosion = count_box + sum_undo_box;
        sum_undo_laserDied = count_explosion + sum_undo_explosion;
        sum_undo_teleport = count_laserDied + sum_undo_laserDied;

        #region 10 for
        for (int i = 0; i < count_blue_Lazer; i++)
        {
            GameObject temp = (GameObject)Instantiate(blue_Lazer);
            temp.SetActive(false);
            temp.transform.SetParent(objectsParent);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_red_Lazer; i++)
        {
            GameObject temp = (GameObject)Instantiate(red_Lazer);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_mine; i++)
        {
            GameObject temp = (GameObject)Instantiate(mine);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_transport; i++)
        {
            GameObject temp = (GameObject)Instantiate(transport);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_turrel; i++)
        {
            GameObject temp = (GameObject)Instantiate(turrel);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_orb; i++)
        {
            GameObject temp = (GameObject)Instantiate(orb);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_box; i++)
        {
            GameObject temp = (GameObject)Instantiate(box);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_explosion; i++)
        {
            GameObject temp = (GameObject)Instantiate(explosion);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_laserDied; i++)
        {
            GameObject temp = (GameObject)Instantiate(laserDied);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }

        for (int i = 0; i < count_teleport; i++)
        {
            GameObject temp = (GameObject)Instantiate(teleport);
            temp.transform.SetParent(objectsParent);
            temp.SetActive(false);
            All_objects.Add(temp);
        }
        #endregion

        for (int i = 0; i < count_objects; i++)
        {
            All_objects_cache.Add(All_objects[i].transform);
        }

        sum_on_audio = sum_undo_laserDied - sum_undo_mine;

        for (int i = sum_undo_mine; i < sum_undo_laserDied; i++)
        {
            //audio_cache.Add();

            audio_cache.Add(All_objects_cache[i].GetComponent<AudioSource>());
        }
    }

    public void Launch_prefab(int choise, Vector3 vectorXY, Quaternion rot)
    {
        if (All_objects != null)
        {
            switch (choise)
            {
                case 1:
                    from = sum_undo_blue_Lazer;
                    to = sum_undo_red_Lazer;
                    break;

                case 2:
                    from = sum_undo_red_Lazer;
                    to = sum_undo_mine;
                    break;

                case 3:
                    from = sum_undo_mine;
                    to = sum_undo_transport;
                    break;

                case 4:
                    from = sum_undo_transport;
                    to = sum_undo_turrel;
                    break;

                case 5:
                    from = sum_undo_turrel;
                    to = sum_undo_orb;
                    break;

                case 6:
                    from = sum_undo_orb;
                    to = sum_undo_box;
                    break;

                case 7:
                    from = sum_undo_box;
                    to = sum_undo_explosion;
                    break;

                case 8:
                    from = sum_undo_explosion;
                    to = sum_undo_laserDied;
                    break;

                case 9:
                    from = sum_undo_laserDied;
                    to = sum_undo_teleport;
                    break;

                case 10:
                    from = sum_undo_teleport;
                    to = count_objects;
                    break;
            }
            
            for (int i = from; i < to; i++)
            {
                if (!All_objects[i].activeInHierarchy)
                {
                    All_objects_cache[i].position = vectorXY;
                    All_objects_cache[i].rotation = rot;
                    All_objects[i].SetActive(true);
                    
                    break;
                }
            }
        }
    }

    public void Changed_Toggle(bool mmute)
    {
        for (int i = 0; i < sum_on_audio; i++)
        {
            audio_cache[i].mute = mmute;
        }
        
    }

    public void Changed_Slider(float sslider)
    {
        for (int i = 0; i < sum_on_audio; i++)
        {
            audio_cache[i].volume = sslider;
        }
    }

    public void Laser_off()
    {
        for (int i = 0; i < sum_undo_mine; i++)
        {
            All_objects[i].SetActive(false);
        }
    }
}


