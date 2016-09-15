using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
    public Light playerLight;
    public Light DayTimeLight;
    public float fadeOutSpeed;
    public float speedFade;
    public float speedFadeOut;
    public float speedAngleFadeOut;
    private bool isFadingOut = false;
    private bool speedBoosterActive = false;
    private bool speedBoosterFadeOut = false;

    public void setDayTime() {
        //do stuff for day time
        playerLight.intensity = 0;
        DayTimeLight.intensity = 3.5f;
    }

    public void setNightTime() {
        //do stuff for night time
        //playerLight.intensity = 8;
        isFadingOut = true;
        //DayTimeLight.intensity = 0;
    }

    void Update() {
        if (isFadingOut) {
            DayTimeLight.intensity = Mathf.Lerp(DayTimeLight.intensity, 0f, fadeOutSpeed * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 8f, fadeOutSpeed * Time.deltaTime);

            if (DayTimeLight.intensity <= 0 && playerLight.intensity == 8) {
                isFadingOut = false;
            }
        }

        if (speedBoosterActive) {
            playerLight.spotAngle = Mathf.Lerp(playerLight.spotAngle, 179, speedFade * Time.deltaTime);
            playerLight.range = Mathf.Lerp(playerLight.range, 25, speedFade * Time.deltaTime);

            if (playerLight.spotAngle == 179 && playerLight.range == 25) {
                speedBoosterActive = false;
            }
        }

        if (speedBoosterFadeOut) {
            playerLight.spotAngle -= speedAngleFadeOut;
            playerLight.range -= speedFadeOut;

            if (playerLight.spotAngle <= 87){
                playerLight.spotAngle = 87;
            }

            if (playerLight.range <= 10) {
                playerLight.range = 10;
            }

            if (playerLight.spotAngle <= 87 && playerLight.range <= 10) {
                speedBoosterFadeOut = false;
            }
        }
    }

    public void IncreasePlayerLightRange() {
        speedBoosterActive = true;
    }

    public void DecreasePlayerLightRange() {
        speedBoosterActive = false;
        speedBoosterFadeOut = true;
    }
}
