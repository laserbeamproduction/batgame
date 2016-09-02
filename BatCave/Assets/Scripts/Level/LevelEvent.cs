using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LevelEvent : MonoBehaviour {
    public DayNightCycle dayNightCycle;

    void Awake()
    {

    }

    void OnEnable()
    {
        EventManager.StartListening(EventTypes.SET_DAY_TIME, setDay);
        EventManager.StartListening(EventTypes.SET_NIGHT_TIME, setNight);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventTypes.SET_DAY_TIME, setDay);
        EventManager.StopListening(EventTypes.SET_NIGHT_TIME, setNight);
    }

    void setDay() {
        dayNightCycle.setDayTime();
    }

    void setNight() {
        dayNightCycle.setNightTime();
    }
}
