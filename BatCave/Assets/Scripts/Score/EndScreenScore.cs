using UnityEngine;
using UnityEngine.UI;

public class EndScreenScore : MonoBehaviour {

    public Text totalGamesPlayedTextField;
    public Text lastScoreTextField;

	// Use this for initialization
	void Start () {
        SaveLoadController.GetInstance().GetPlayer().AddTotalGamesPlayed(1);
        totalGamesPlayedTextField.text = "Games: " + SaveLoadController.GetInstance().GetPlayer().GetTotalGamesPlayed();

        lastScoreTextField.text = "Your Score: " + PlayerPrefs.GetFloat("playerScore").ToString();
        
    }
}
