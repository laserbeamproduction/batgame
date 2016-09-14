using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
    public Light playerLight;
    public Light DayTimeLight;
    public float fadeOutSpeed;
    private bool isFadingOut = false;

    public void setDayTime() {
        //do stuff for day time
        playerLight.intensity = 0;
        DayTimeLight.intensity = 3.5f;
    }

    public void setNightTime() {
        //do stuff for night time
        playerLight.intensity = 8;
        isFadingOut = true;
        //DayTimeLight.intensity = 0;
    }

    void Update() {
        if (isFadingOut) {
            DayTimeLight.intensity = Mathf.Lerp(DayTimeLight.intensity, 0f, fadeOutSpeed * Time.deltaTime); ;

            if (DayTimeLight.intensity <= 0) {
                isFadingOut = false;
            }
        }
    }
}
