using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    public float duration;

    private float timer = 0f;
    private bool readyForContinue;

    // Kinda heckish.. TODO: Clean this mess up.
    // Google Play Helper sets this bool on false when login was succesfull and savegame is loaded. 
    private static bool onStartUp = true;

    void Start()
    {
        if (onStartUp && !Application.isEditor)
        {
            onStartUp = false;
            GooglePlayHelper gph = GooglePlayHelper.GetInstance();
            GooglePlayHelper.GetInstance().Login();
            AchievementChecker.CheckForWelcomeAchievement();
        }

    }

// Update is called once per frame
void Update () {
        timer += Time.deltaTime;
        if (timer >= duration && !readyForContinue) {
            readyForContinue = true;
            LoadingController.LoadScene(LoadingController.Scenes.MAIN_MENU);
        }
	}
}
