using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public Button button;
    public Slider slider;
    public Text text;

    private AsyncOperation AsOp;
    private AsyncOperation AsOpM;
    private float loading_progress = 0f;
    private float round_load;
    private int load_lvl;
    public float A1;
    public float A2;

    void Start()
    {
        StartCoroutine("loading_lvl");
    }

    IEnumerator loading_lvl()
    {
        if (PlayerPrefs.HasKey("load_lvl"))
        {
            load_lvl = PlayerPrefs.GetInt("load_lvl");
            AsOp = SceneManager.LoadSceneAsync(load_lvl);
            AsOp.allowSceneActivation = false;
            AsOpM = SceneManager.LoadSceneAsync("Scene0");
            AsOpM.allowSceneActivation = false;
        }

        while (AsOp.isDone == false)
        {
            A1 = AsOp.progress;
            A2 = AsOp.progress;
            loading_progress = AsOp.progress * 100f;
            if (loading_progress == 90)
            {
                button.gameObject.SetActive(true);
                loading_progress = 100;
            }
            round_load = Mathf.RoundToInt(loading_progress);

            slider.value = round_load / 100;
            text.text = "Loading " + round_load.ToString() + '%';

            yield return true;
        }
    }

    public void Allow()
    {
        AsOp.allowSceneActivation = true;
    }
}
