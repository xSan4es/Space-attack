using UnityEngine;

public class SearchVariable : MonoBehaviour
{
    public static Transform new_parent;
    public static GameObject leftCorner;
    public static GameObject rightCorner;
    public static GameObject upCorner;
    public static GameObject downCorner;

    public static Transform player;
    public static Transform targetMines;
    public static Transform portation;
    public static Transform targetTransport;

    public static Lose lose;
    public static Pool pool;
    public static IGameController controller;
    public static SearchVariable variables;
    public static HeroGo heroGo;
    public static int destroyed_enemy;
    public static int all_enemy_dest;

    public int rand;
    public float repeat_time;
    public float curr_time;
    public bool go_timer;

    private int range;

    void Awake ()
    {
        TransportGo.speed = 1.25f;
        MinesGo.speed = 1.75f;
        Lose.WIN = false;

        repeat_time = 5f;
        go_timer = false;
        rand = 10;

        destroyed_enemy = 0;
        all_enemy_dest = 0;

        /*leftCorner = null;
        rightCorner = null;
        upCorner = null;
        downCorner = null;

        player = null;
        targetMines = null;
        portation = null;

        targetTransport = null;
        lose = null;

        controller = null;
        pool = null;
        variables = null;*/

        new_parent = GameObject.Find("Zoom").transform;
        lose = GameObject.FindGameObjectWithTag("GameController").GetComponent<Lose>();
        pool = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pool>(); 
        variables = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SearchVariable>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<IGameController>();
        heroGo = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroGo>();

        leftCorner = GameObject.Find("leftCorner");
        rightCorner = GameObject.Find("rightCorner");
        upCorner = GameObject.Find("upCorner");
        downCorner = GameObject.Find("downCorner");

        targetMines = GameObject.Find("MinesPortalIn").transform; // ціль монстрів
        player = GameObject.Find("Hero").transform; // ціль монстрів
        portation = GameObject.Find("MinesPortalOut").transform; // ціль монстрів 
        targetTransport = GameObject.Find("TransportPortal").transform; // ціль монстрів

        if (controller.lvl() == 2) range = 5;
        else range = 6; 
    }

    void Update()
    {
        if (go_timer)
        {
            curr_time -= Time.deltaTime;
            if (curr_time <= 0)
            {
                go_timer = false;
                Random_Action_Return(rand);
                rand = 10;
            }
        }
    }

    public void Random_Action()
    {
        if (rand != 10)
        {
            Random_Action_Return(rand);
        }

        rand = Random.Range(0, range);
        curr_time = repeat_time;
        go_timer = true;
        switch (rand + 1)
        {
            case 1:
                heroGo.timeBetweenFires = heroGo.timeBetweenFires / 2;
                break;

            case 2:
                heroGo.max_speed_set(8f);
                break;

            case 3:
                heroGo.Shield_On();
                break;

            case 4:
                MinesGo.Take_Speed();
                break;

            case 5:
                TransportGo.Add_Speed();
                break;

            case 6:
                TurretGo.Add_Speed();
                break;

            case 7:
                ;
                break;
        }
    }

    void Random_Action_Return(int choise)
    {
        switch (choise + 1)
        {
            case 1:
                heroGo.timeBetweenFires = heroGo.timeBetweenFires * 2;
                break;

            case 2:
                heroGo.max_speed_set(4f);
                if (heroGo.cur_speed() > 4f) heroGo.cur_speed_set(4f);
                break;

            case 3:
                heroGo.Shield_Off();
                break;

            case 4:
                MinesGo.Add_Speed();
                break;

            case 5:
                TransportGo.Take_Speed();
                break;

            case 6:
                TurretGo.Take_Speed();
                break;

            case 7:
                ;
                break;
        }
    }

}
