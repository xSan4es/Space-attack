using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    
    public Text score;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Transport")
        {
            SearchVariable.all_enemy_dest++;
            col.gameObject.SetActive(false);
            SearchVariable.controller.EnemyWinAdd();
            score.text = SearchVariable.controller.EnemyWin().ToString() + "/3"; 
            if (SearchVariable.controller.EnemyWin() < 3 && SearchVariable.all_enemy_dest == SearchVariable.controller.Need_to_end()) SearchVariable.controller.end_game(); //Lose.WIN = true;
        }
    }
}
