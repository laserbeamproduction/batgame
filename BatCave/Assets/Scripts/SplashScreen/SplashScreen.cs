using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    public float duration;

    private float timer = 0f;
    private bool readyForContinue;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= duration && !readyForContinue) {
            readyForContinue = true;
            LoadingController.LoadScene(LoadingController.Scenes.MAIN_MENU);
        }
	}
}
