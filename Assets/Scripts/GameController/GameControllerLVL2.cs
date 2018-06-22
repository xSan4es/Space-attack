﻿using UnityEngine;
using System.Collections;

public class GameControllerLVL2 : MonoBehaviour, IGameController
{
    private int creating_amount_transport = 0;
    public int need_to_end = 5;
    private int ii;

    public GameObject endGame;
    public float timeBeforeSpawning;
    public Transform respawnMines;

    private float randDistance_x;
    private float randDistance_y;

    public new Camera camera;
    private int enemyWin;

    private Transform myTransform;

    void Start()
    {
        ii = 0;
        
        myTransform = transform;

        creating_amount_transport = 0;

        enemyWin = 0;

        StartCoroutine(SpawnEnemies()); // починає створювати хвилю, при виконанні умови (к-ть ворогів на полі)
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBeforeSpawning);
        while (true)
        {
            while (true)
            {
                ii++;

                if (ii % 3 == 0) SearchVariable.pool.Launch_prefab(3, new Vector3(respawnMines.transform.position.x,
                    respawnMines.transform.position.y, 0), myTransform.rotation);

                if (creating_amount_transport == need_to_end) yield break;

                if (ii % 10 == 0 && creating_amount_transport < need_to_end)
                {
                    SearchVariable.pool.Launch_prefab(4, new Vector3(respawnMines.transform.position.x,
                        respawnMines.transform.position.y, 0), myTransform.rotation);

                    creating_amount_transport++;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void end_game()
    {
        endGame.SetActive(true);
    }

    public int EnemyWin()
    {
        return enemyWin;
    }

    public void EnemyWinAdd()
    {
        enemyWin++;
    }

    public int Creating_amount_transport()
    {
        return creating_amount_transport;
    }

    public int Need_to_end()
    {
        return need_to_end;
    }

    public Vector3 placing()
    {
        return new Vector3(
                    SearchVariable.portation.transform.position.x + 2 * Random.Range(0, 9) * camera.ScreenToWorldPoint(new Vector3(Screen.width / 1.1f - SearchVariable.portation.transform.position.x, 0, 0)).x / 14,
                    SearchVariable.portation.transform.position.y,
                    0);
    }

    public int lvl()
    {
        return 2;
    }

    public void create_turrel() { }
}