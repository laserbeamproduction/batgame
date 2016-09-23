using UnityEngine;
using System.Collections;
using System;

public class DayNightCycle : MonoBehaviour {
    public Light playerLight;
    public Light DayTimeLight;
    public float fadeOutSpeed;
    public float speedFade;
    public float speedFadeOut;
    public float speedAngleFadeOut;
    private bool isFadingOut = false;
    private bool isFadingIn = false;
    private bool speedBoosterActive = false;
    private bool speedBoosterFadeOut = false;

    void Start() {
        EventManager.StartListening(EventTypes.TRANSITION_START, OnTransitionStart);
        EventManager.StartListening(EventTypes.TRANSITION_END, OnTransitionEnd);
    }

    void OnTransitionEnd(object arg0) {
        setNightTime();
    }

    void OnTransitionStart(object arg0) {
        setDayTime();
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.TRANSITION_START, OnTransitionStart);
        EventManager.StopListening(EventTypes.TRANSITION_END, OnTransitionEnd);
    }

    public void setDayTime() {
        isFadingIn = true;
    }

    public void setNightTime() {
        isFadingOut = true;
    }

    void Update() {
        if (isFadingOut) {
            DayTimeLight.intensity = Mathf.Lerp(DayTimeLight.intensity, 0f, fadeOutSpeed * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 8f, fadeOutSpeed * Time.deltaTime);

            if (DayTimeLight.intensity <= 0f && playerLight.intensity == 8f) {
                isFadingOut = false;
            }
        } else if (isFadingIn) {
            DayTimeLight.intensity = Mathf.Lerp(DayTimeLight.intensity, 8f, fadeOutSpeed * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 0f, fadeOutSpeed * Time.deltaTime);

            if (DayTimeLight.intensity >= 7f && playerLight.intensity == 0f) {
                isFadingIn = false;
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
