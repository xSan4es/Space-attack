using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
    public void WinGame()
    {
        SearchVariable.lose.Win();
    }

    public void create_turrel()
    {
        SearchVariable.controller.create_turrel();
        gameObject.SetActive(false);
    }

    public void lose()
    {
        SearchVariable.lose.lose_game();
    }
}
