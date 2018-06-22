using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Lose : MonoBehaviour
{
    public static bool WIN;
    public GameObject Teleport;
    public GameObject restartDialog;
    public GameObject Return;
    public GameObject Next;
    public GameObject[] Hide = new GameObject[3];
    public GameObject[] Hide2 = new GameObject[3];
    public GameObject win;
    public Text WinTxT;
    //public AudioClip ButtonClickSound;
    public AudioSource[] AOAS;
    private float pausedTime;
    public float curr_time;
    private bool plus_chack;
    private bool lose;

    //private AsyncOperation AsOp;


    void Start()
    {
        //AsOp = SceneManager.LoadSceneAsync(0);
        //AsOp.allowSceneActivation = false;

        lose = false;
        plus_chack = false;
        curr_time = 0;
        win.SetActive(false);
        restartDialog.SetActive(false);
        for (int i = 0; i < Hide.Length; i++)
        {
            Hide[i].SetActive(true);
        }
        Time.timeScale = 1;
    }

    void menu_pause()
    {
            if (Time.timeScale > 0)
            {
                pausedTime = Time.timeScale;
                Time.timeScale = 0;

            AOAS = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            for (int i = 0; i < AOAS.Length; i++)
            {
                AOAS[i].Pause();
            }
            }

            else
            {
                for (int i = 0; i < AOAS.Length; i++)
                {
                    AOAS[i].UnPause();
                }
                Time.timeScale = pausedTime;
            }
    }
    
    void FixedUpdate ()
    {
        curr_time += Time.fixedDeltaTime;

        if (lose || SearchVariable.controller.EnemyWin() > 2)
        {
            float timme = PlayerPrefs.GetFloat("time_in_game");
            timme += curr_time;
            PlayerPrefs.SetFloat("time_in_game", timme);
            plus_chack = true;

            Return.SetActive(false);
            restartDialog.SetActive(true);
            WinTxT.text = "YOU  LOSE!!!";
            win.SetActive(true);

            for (int i = 0; i < Hide.Length; i++)
            {
                Hide[i].SetActive(false);
                Hide2[i].SetActive(false);
            }

            menu_pause();
        }

        if (HeroGo.health > 0 && SearchVariable.controller.EnemyWin() < 3 && WIN == true)
            TeleportHero();
	}
    
    public void RestartGame()
    {
        //GetComponent<AudioSource>().PlayOneShot(ButtonClickSound);
        if (!plus_chack)
        {
            float timme = PlayerPrefs.GetFloat("time_in_game");
            timme += curr_time;
            PlayerPrefs.SetFloat("time_in_game", timme);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //PlayerPrefs.SetInt("load_lvl", SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("SceneLoad");
    }

    public void ExitToMenu()
    {
        //GetComponent<AudioSource>().PlayOneShot(ButtonClickSound);

        /*PlayerPrefs.SetInt("load_lvl", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SceneLoad");*/
        if (!plus_chack)
        {
            float timme = PlayerPrefs.GetFloat("time_in_game");
            timme += curr_time;
            PlayerPrefs.SetFloat("time_in_game", timme);
        }
        SceneManager.LoadScene(0);
        //AsOp.allowSceneActivation = true;
    }

    public void Pause()
    {
        restartDialog.SetActive(true);
        Return.SetActive(true);
        WinTxT.text = "Cosmos Attack";
        win.SetActive(true);

        for (int i = 0; i < Hide.Length; i++)
        {
            Hide2[i].SetActive(true);
            Hide[i].SetActive(false);
        }

        menu_pause();
    }

    public void Return_Game()
    {
        restartDialog.SetActive(false);
        Return.SetActive(false);

        for (int i = 0; i < Hide.Length; i++)
        {
            Hide[i].SetActive(true);
        }

        menu_pause();
    }

    public void TeleportHero()
    {
        Teleport.SetActive(true);
        SearchVariable.pool.Laser_off();
    }

    public void load_next_lvl()
    {
        PlayerPrefs.SetInt("load_lvl", SearchVariable.controller.lvl()+1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SceneLoad");
    }

    public void Win()
    {
        if (PlayerPrefs.GetInt("lvl_open") < (SearchVariable.controller.lvl() + 0.5f))
        {
            PlayerPrefs.SetInt("lvl_open", (SearchVariable.controller.lvl()+1));
            PlayerPrefs.Save();
        }

        Next.SetActive(true);
        float timme = PlayerPrefs.GetFloat("time_in_game");
        timme += curr_time;
        PlayerPrefs.SetFloat("time_in_game", timme);
        plus_chack = true;

        Return.SetActive(false);
        restartDialog.SetActive(true);
        WinTxT.text = "YOU  WIN!!!";
        win.SetActive(true);

        for (int i = 0; i < Hide.Length; i++)
        {
            Hide[i].SetActive(false);
            Hide2[i].SetActive(false);
        }

        menu_pause();
    }

    public void lose_game()
    {
        lose = true;
    }
}
