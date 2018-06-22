using UnityEngine;

public interface IGameController
{
    //void KilledEnemy();

    void end_game();

    int EnemyWin();

    int Creating_amount_transport();

    int Need_to_end();

    void EnemyWinAdd();

    Vector3 placing();

    void create_turrel();

    int lvl();
}
