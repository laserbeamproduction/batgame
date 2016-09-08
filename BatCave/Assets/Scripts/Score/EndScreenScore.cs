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
        lastScoreTextField.text = player.GetCurrentSessionScore().ToString();
    }

    void Update() {
        if (!addShown) {
            if (Random.Range(1, 101) <= 20)
            {
                ShowAd();
            }
            else {
                addShown = true;
            }
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
