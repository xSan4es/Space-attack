using UnityEngine;
using UnityEngine.UI;

public class audioANDcontroller : MonoBehaviour
{
    public AudioSource main;
    public AudioSource hero;

    public Dropdown dropdown;
    public Toggle toggle;
    public Slider slider;

    void Start()
    {
        dropdown.value = PlayerPrefs.GetInt("isDoubleJoystickController");
        
        slider.value = PlayerPrefs.GetFloat("Slider");
        
        if (PlayerPrefs.GetInt("Toggle") == 0) toggle.isOn = false;
        else if (PlayerPrefs.GetInt("Toggle") == 1) toggle.isOn = true;

        Changed_controller();
        Changed_Slider();
        Changed_Toggle();
    }

    public void Changed_Toggle()
    {
        main.mute = toggle.isOn;
        hero.mute = toggle.isOn;
        SearchVariable.pool.Changed_Toggle(toggle.isOn);

        if (toggle.isOn == false) PlayerPrefs.SetInt("Toggle", 0);
        else if (toggle.isOn == true) PlayerPrefs.SetInt("Toggle", 1);
        PlayerPrefs.Save();
    }

    public void Changed_Slider()
    {
        main.volume = slider.value;
        hero.volume = slider.value;
        SearchVariable.pool.Changed_Slider(slider.value);

        PlayerPrefs.SetFloat("Slider", slider.value);
        PlayerPrefs.Save();
    }

    public void Changed_controller()
    {
        HeroGo.IsDoubleJoystickController = dropdown.value;
        PlayerPrefs.SetInt("isDoubleJoystickController", dropdown.value);
        PlayerPrefs.Save();
    }
}
