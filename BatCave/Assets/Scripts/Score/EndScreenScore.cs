using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class EndScreenScore : MonoBehaviour {

    public Text totalGamesPlayedTextField;
    public Text lastScoreTextField;
    private bool addShown = false;

	// Use this for initialization
	void Start () {
        PlayerSave player = SaveLoadController.GetInstance().GetPlayer();
      //  totalGamesPlayedTextField.text = "Games: " + player.GetTotalGamesPlayed();
        lastScoreTextField.text = "Your Score: " + player.GetCurrentSessionScore();
    }

    void Update() {
        if (!addShown) {
            ShowAd();
        }
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady() && !addShown)
        {
            Advertisement.Show();
            addShown = true;
        }
    }
}
