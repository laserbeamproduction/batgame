using UnityEngine;
using UnityEngine.UI;

public class EndScreenScore : MonoBehaviour {

    public Text totalGamesPlayedTextField;
    public Text lastScoreTextField;

	// Use this for initialization
	void Start () {
        PlayerSave player = SaveLoadController.GetInstance().GetPlayer();
      //  totalGamesPlayedTextField.text = "Games: " + player.GetTotalGamesPlayed();
        lastScoreTextField.text = player.GetCurrentSessionScore().ToString();
    }
}
