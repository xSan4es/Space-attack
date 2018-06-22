using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public Image[] R_of_L = new Image[2];
    public GameObject[] panel_GM = new GameObject[3];
    public GameObject[] achi_GM = new GameObject[2];
    public Animator[] panel_anim = new Animator[3];
    public Animator[] achi_anim = new Animator[2];
    private int panel_lvl;
    private int achi_lvl;
    public Text kills;
    public AudioSource main;
    public bool sett_onORoff;

    private Transform[] ach_component = new Transform[16];
    private GameObject[] ach_GameObject = new GameObject[16];
    private GameObject[] ach_GameObject_child = new GameObject[16];

    private Sprite sprite_lvl_gray;
    private Sprite sprite_achi_gray;
    private Sprite sprite_block;
    private Sprite sprite_RofL;
    private Sprite sprite_RofL2;

    public GameObject[] gameObjectLVL = new GameObject[17];
    public GameObject[] gameObjectLVL_plus = new GameObject[17];
    
    public GameObject Hiden;
    public GameObject achivements;
    public GameObject achivements2;

    public Dropdown dropdown;
    public Toggle toggle;
    public Slider slider;

    public Animator startButton;
    public Animator survivalButton;
    public Animator settingsButton;
    public Animator Exitt;
    public Animator dialog;
    public Animator achivement;
    public Animator top;
    public Animator info;
    public Animator LVLdialog;
    public Animator contentPanel;
    public Animator gearImage;
    //public AudioClip ButtonClickSound;
    bool check;
    //public AudioSource AOAS;
    private RectTransform ttransform;


    public void Start()
    {
        //PlayerPrefs.SetInt("killedEnemy", 32);
        panel_lvl = 1;
        achi_lvl = 1;
        check = false;
        sett_onORoff = false;

        sprite_lvl_gray = Sprite.Create(Resources.Load<Texture2D>("BTN_BLUE_SQ_BW"), new Rect(0, 0, 114, 94), new Vector2(0.5f, 0.5f), 25f);
        sprite_achi_gray = Sprite.Create(Resources.Load<Texture2D>("blank_b&w"), new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f), 25f);
        sprite_block = Sprite.Create(Resources.Load<Texture2D>("00065"), new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f), 25f);
        sprite_RofL = Sprite.Create(Resources.Load<Texture2D>("BTN"), new Rect(0, 0, 45, 52), new Vector2(0.5f, 0.5f), 25f);
        sprite_RofL2 = Sprite.Create(Resources.Load<Texture2D>("BTN2"), new Rect(0, 0, 45, 52), new Vector2(0.5f, 0.5f), 25f);

        Time.timeScale = 1;
        ttransform = contentPanel.gameObject.transform as RectTransform;
        Vector2 position = ttransform.anchoredPosition;
        position.y -= ttransform.rect.height;
        ttransform.anchoredPosition = position;
        
        if (!PlayerPrefs.HasKey("isDoubleJoystickController"))
        {
            PlayerPrefs.SetInt("isDoubleJoystickController", 0);
        }
        dropdown.value = PlayerPrefs.GetInt("isDoubleJoystickController");
        
        if (!PlayerPrefs.HasKey("Slider"))
        {
            PlayerPrefs.SetFloat("Slider", 1f);
        }
        slider.value = PlayerPrefs.GetFloat("Slider");
        
        if (!PlayerPrefs.HasKey("Toggle"))
        {
            PlayerPrefs.SetInt("Toggle", 1);
        }
        if (PlayerPrefs.GetInt("Toggle") == 0) toggle.isOn = false;
        else if (PlayerPrefs.GetInt("Toggle") == 1) toggle.isOn = true;

        Changed_controller();
        Changed_Slider();
        Changed_Toggle();

        if (!PlayerPrefs.HasKey("killedEnemy"))
        {
            PlayerPrefs.SetInt("killedEnemy", 0);
        }

        if (!PlayerPrefs.HasKey("time_in_game"))
        {
            PlayerPrefs.SetFloat("time_in_game", 0);
        }

        int time = (int)PlayerPrefs.GetFloat("time_in_game") / 60;

        kills.text = "You killed " + PlayerPrefs.GetInt("killedEnemy").ToString() + " enemies" + "\n You play in game " + time.ToString() + " m";

        for (int i = 2; i < 10; i++)
        {
            ach_component[i - 2] = achivements.transform.GetChild(i);
            ach_GameObject[i - 2] = ach_component[i - 2].gameObject;
            ach_component[i - 2] = ach_GameObject[i - 2].transform.GetChild(0);
            ach_GameObject_child[i - 2] = ach_component[i - 2].gameObject;

            ach_component[i + 6] = achivements2.transform.GetChild(i);
            ach_GameObject[i + 6] = ach_component[i + 6].gameObject;
            ach_component[i + 6] = ach_GameObject[i + 6].transform.GetChild(0);
            ach_GameObject_child[i + 6] = ach_component[i + 6].gameObject;
        }

        block_achi();
        PlayerPrefs.Save();
    }

    public void Right_achi()
    {
        if (achi_lvl < 2)
        {
            achi_GM[0].SetActive(true);
            achi_GM[1].SetActive(true);
            if (achi_lvl == 1)
            {
                R_of_L[2].sprite = sprite_RofL2;
                R_of_L[3].sprite = sprite_RofL;
                achi_anim[achi_lvl - 1].SetInteger("anim", 1);
                achi_anim[achi_lvl].SetInteger("anim", 1);
            }
            else
            {
                R_of_L[3].sprite = sprite_RofL;
                achi_anim[achi_lvl - 1].SetInteger("anim", 2);
                achi_anim[achi_lvl].SetInteger("anim", 1);
            }
            achi_lvl++;
        }
    }

    public void Left_achi()
    {
        if (achi_lvl > 1)
        {
            achi_GM[0].SetActive(true);
            achi_GM[1].SetActive(true);
            if (achi_lvl == 2)
            {
                R_of_L[2].sprite = sprite_RofL;
                R_of_L[3].sprite = sprite_RofL2;
                achi_anim[achi_lvl - 1].SetInteger("anim", 3);
                achi_anim[achi_lvl - 2].SetInteger("anim", 3);
            }
            else
            {
                R_of_L[3].sprite = sprite_RofL2;
                achi_anim[achi_lvl - 1].SetInteger("anim", 3);
                achi_anim[achi_lvl - 2].SetInteger("anim", 4);
            }

            achi_lvl--;
        }
    }

    public void Right()
    {
        if (panel_lvl < 3)
        {
            panel_GM[0].SetActive(true);
            panel_GM[1].SetActive(true);
            panel_GM[2].SetActive(true);

            if (panel_lvl == 1)
            {
                R_of_L[0].sprite = sprite_RofL2;
                panel_anim[panel_lvl - 1].SetInteger("anim", 1);
                panel_anim[panel_lvl].SetInteger("anim", 1);
            }
            else
            {
                R_of_L[1].sprite = sprite_RofL;
                panel_anim[panel_lvl - 1].SetInteger("anim", 2);
                panel_anim[panel_lvl].SetInteger("anim", 1);
            }
            panel_lvl++;
        }
    }

    public void Left()
    {
        if (panel_lvl > 1)
        {
            panel_GM[0].SetActive(true);
            panel_GM[1].SetActive(true);
            panel_GM[2].SetActive(true);

            if (panel_lvl == 2)
            {
                R_of_L[0].sprite = sprite_RofL;
                panel_anim[panel_lvl - 1].SetInteger("anim", 3);
                panel_anim[panel_lvl - 2].SetInteger("anim", 3);
            }
            else
            {
                R_of_L[1].sprite = sprite_RofL2;
                panel_anim[panel_lvl - 1].SetInteger("anim", 3);
                panel_anim[panel_lvl - 2].SetInteger("anim", 4);
            }
            
            panel_lvl--;
        }
    }

    public void Link()
    {
        Application.OpenURL("https://vk.com/id28703940");
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("killedEnemy", 0);
        PlayerPrefs.SetInt("lvl_open", 1);
        PlayerPrefs.SetFloat("time_in_game", 0);
        PlayerPrefs.Save();
        block_achi();
        kills.text = "You killed 0 enemies" + "\n You play in game 0 m";
    }

    public void Open_all()
    {
        PlayerPrefs.SetInt("lvl_open", 18);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        //GetComponent<AudioSource>().PlayOneShot(ButtonClickSound);
        startButton.SetBool("isHidden", true);
        settingsButton.SetBool("isHidden", true);
        Exitt.SetBool("isHidden", true);
        survivalButton.SetBool("isHidden", true);
        LVLdialog.enabled = true;
        LVLdialog.SetBool("isHidden", true);
        
        if (sett_onORoff == true) ToggleMenu();
        Hiden.SetActive(false);

        if (!PlayerPrefs.HasKey("lvl_open"))
        {
            PlayerPrefs.SetInt("lvl_open", 1);
        }
        for (int i = 0; i < 17; i++)
        {
            if (PlayerPrefs.GetInt("lvl_open") < 1.5f + i)
            {
                gameObjectLVL[i].GetComponent<Image>().sprite = sprite_lvl_gray;
                gameObjectLVL_plus[i].SetActive(true);
            }
        }
    }

    public void CloseSettingsLVL()
    {
        if (panel_lvl != 1) panel_GM[0].SetActive(false);
        if (panel_lvl != 2) panel_GM[1].SetActive(false);
        if (panel_lvl != 3) panel_GM[2].SetActive(false);
        startButton.SetBool("isHidden", false);
        settingsButton.SetBool("isHidden", false);
        survivalButton.SetBool("isHidden", false);
        Exitt.SetBool("isHidden", false);
        LVLdialog.SetBool("isHidden", false);
        LVLdialog.SetBool("isHidden2", true);
        Hiden.SetActive(true);
        if (sett_onORoff == true) ToggleMenu();
        contentPanel.enabled = false;
        Vector2 position = ttransform.anchoredPosition;
        position.y -= ttransform.rect.height;
        ttransform.anchoredPosition = position;
        gearImage.enabled = false;
    }

    public void OpenSettings()
    {
        //GetComponent<AudioSource>().PlayOneShot(ButtonClickSound);

        startButton.SetBool("isHidden", true);
        settingsButton.SetBool("isHidden", true);
        survivalButton.SetBool("isHidden", true);
        Exitt.SetBool("isHidden", true);
        dialog.enabled = true;
        dialog.SetBool("isHidden", true);
        if (sett_onORoff == true) ToggleMenu();
        Hiden.SetActive(false);
    }

    public void CloseSettings()
    {
        //GetComponent<AudioSource>().PlayOneShot(ButtonClickSound);

        startButton.SetBool("isHidden", false);
        settingsButton.SetBool("isHidden", false);
        survivalButton.SetBool("isHidden", false);
        Exitt.SetBool("isHidden", false);
        dialog.SetBool("isHidden", false);
        dialog.SetBool("idHidden2", true);
        Hiden.SetActive(true);

        contentPanel.enabled = false;
        Vector2 position = ttransform.anchoredPosition;
        position.y -= ttransform.rect.height;
        ttransform.anchoredPosition = position;
        gearImage.enabled = false;

        /*contentPanel.enabled = false;
        gearImage.enabled = false;*/
    }

    public void ToggleMenu()
    {
        //GetComponent<AudioSource>().PlayOneShot(ButtonClickSound);

        sett_onORoff = !sett_onORoff;
        contentPanel.enabled = true;
        bool isHidden = contentPanel.GetBool("isHidden");
        contentPanel.SetBool("isHidden", !isHidden);
        gearImage.enabled = true;
        gearImage.SetBool("isHidden", !isHidden);

        if (check == true)
        {
            contentPanel.SetBool("isHidden2", true);
            gearImage.SetBool("isHidden2", true);
        }
        check = true;
    }

    public void Changed_controller()
    {
        PlayerPrefs.SetInt("isDoubleJoystickController", dropdown.value);
        PlayerPrefs.Save();
    }

    public void Changed_Toggle()
    {
        main.mute = toggle.isOn;
        if (toggle.isOn == false) PlayerPrefs.SetInt("Toggle", 0);
        else if (toggle.isOn == true) PlayerPrefs.SetInt("Toggle", 1);
        PlayerPrefs.Save();
    }

    public void Changed_Slider()
    {
        main.volume = slider.value;
        PlayerPrefs.SetFloat("Slider", slider.value);
        PlayerPrefs.Save();
    }

    private void block_achi()
    {
        if (!PlayerPrefs.HasKey("killedEnemy"))
        {
            PlayerPrefs.SetInt("killedEnemy", 0);
        }

        int killed = PlayerPrefs.GetInt("killedEnemy");

        if (killed < 10)
        {
            ach_GameObject[0].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[0].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 20)
        {
            ach_GameObject[1].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[1].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 30)
        {
            ach_GameObject[2].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[2].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 40)
        {
            ach_GameObject[3].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[3].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 50)
        {
            ach_GameObject[4].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[4].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 60)
        {
            ach_GameObject[5].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[5].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 70)
        {
            ach_GameObject[6].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[6].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 80)
        {
            ach_GameObject[7].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[7].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 90)
        {
            ach_GameObject[8].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[8].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 100)
        {
            ach_GameObject[9].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[9].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 110)
        {
            ach_GameObject[10].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[10].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 120)
        {
            ach_GameObject[11].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[11].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 130)
        {
            ach_GameObject[12].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[12].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 140)
        {
            ach_GameObject[13].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[13].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 150)
        {
            ach_GameObject[14].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[14].GetComponent<Image>().sprite = sprite_block;
        }

        if (killed < 160)
        {
            ach_GameObject[15].GetComponent<Image>().sprite = sprite_achi_gray;
            ach_GameObject_child[15].GetComponent<Image>().sprite = sprite_block;
        }
    }

    public void Open_Achivement()
    {
        startButton.SetBool("isHidden", true);
        settingsButton.SetBool("isHidden", true);
        Exitt.SetBool("isHidden", true);
        survivalButton.SetBool("isHidden", true);

        ToggleMenu();
        achivement.enabled = true;
        achivement.SetBool("isHidden", false);
        Hiden.SetActive(false);
    }

    public void Open_Info()
    {
        startButton.SetBool("isHidden", true);
        settingsButton.SetBool("isHidden", true);
        Exitt.SetBool("isHidden", true);
        survivalButton.SetBool("isHidden", true);

        ToggleMenu();
        info.enabled = true;
        info.SetBool("isHidden", false);
        Hiden.SetActive(false);
    }

    public void Close_Achivement()
    {
        if (achi_lvl != 1) achi_GM[0].SetActive(false);
        if (achi_lvl != 2) achi_GM[1].SetActive(false);

        startButton.SetBool("isHidden", false);
        settingsButton.SetBool("isHidden", false);
        Exitt.SetBool("isHidden", false);
        survivalButton.SetBool("isHidden", false);

        achivement.SetBool("isHidden", true);
        achivement.SetBool("isHidden2", true);
        Hiden.SetActive(true);
        contentPanel.enabled = false;
        Vector2 position = ttransform.anchoredPosition;
        position.y -= ttransform.rect.height;
        ttransform.anchoredPosition = position;
        gearImage.enabled = false;
    }

    public void Open_Top()
    {
        startButton.SetBool("isHidden", true);
        settingsButton.SetBool("isHidden", true);
        Exitt.SetBool("isHidden", true);
        survivalButton.SetBool("isHidden", true);

        ToggleMenu();
        top.enabled = true;
        top.SetBool("isHidden", false);
        Hiden.SetActive(false);
    }

    public void Close_Top()
    {
        startButton.SetBool("isHidden", false);
        settingsButton.SetBool("isHidden", false);
        Exitt.SetBool("isHidden", false);
        survivalButton.SetBool("isHidden", false);

        top.SetBool("isHidden", true);
        top.SetBool("isHidden2", true);

        Hiden.SetActive(true);
        contentPanel.enabled = false;
        Vector2 position = ttransform.anchoredPosition;
        position.y -= ttransform.rect.height;
        ttransform.anchoredPosition = position;
        gearImage.enabled = false;
    }

    public void Close_Info()
    {
        startButton.SetBool("isHidden", false);
        settingsButton.SetBool("isHidden", false);
        Exitt.SetBool("isHidden", false);
        survivalButton.SetBool("isHidden", false);

        info.SetBool("isHidden", true);
        info.SetBool("isHidden2", true);

        Hiden.SetActive(true);
        contentPanel.enabled = false;
        Vector2 position = ttransform.anchoredPosition;
        position.y -= ttransform.rect.height;
        ttransform.anchoredPosition = position;
        gearImage.enabled = false;
    }

    public void Choise_exit()
    {
        Application.Quit();
    }

    public void Choise_lvl1()
    {
        PlayerPrefs.SetInt("load_lvl", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SceneLoad");    
    }

    public void Choise_lvl2()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 1.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 2);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl3()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 2.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 3);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl4()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 3.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 4);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl5()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 4.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 5);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl6()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 5.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 6);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl7()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 6.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 7);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl8()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 7.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 8);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl9()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 8.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 9);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl10()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 9.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 10);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl11()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 10.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 11);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }

    public void Choise_lvl12()
    {
        if (PlayerPrefs.GetInt("lvl_open") > 11.5f)
        {
            PlayerPrefs.SetInt("load_lvl", 12);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SceneLoad");
        }
    }
}
