using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
    public Light playerLight;
    public Light DayTimeLight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setDayTime() {
        //do stuff for day time
        playerLight.intensity = 0;
        DayTimeLight.intensity = 8;
    }

    public void setNightTime() {
        //do stuff for night time
        playerLight.intensity = 8;
        DayTimeLight.intensity = 0;
    }
}
