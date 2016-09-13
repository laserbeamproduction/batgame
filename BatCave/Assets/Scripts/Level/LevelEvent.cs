using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LevelEvent : MonoBehaviour {
    public DayNightCycle dayNightCycle;
    public PlayerControls playerControls;

    void OnEnable()
    {
        EventManager.StartListening(EventTypes.SET_DAY_TIME, setDay);
        EventManager.StartListening(EventTypes.SET_NIGHT_TIME, setNight);
        EventManager.StartListening(EventTypes.PLAYER_SPEED_PICKUP, SpeedPowerUpActive);
        EventManager.StartListening(EventTypes.PLAYER_SPEED_ENDED, SpeedPowerUpEnded);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventTypes.SET_DAY_TIME, setDay);
        EventManager.StopListening(EventTypes.SET_NIGHT_TIME, setNight);
        EventManager.StopListening(EventTypes.PLAYER_SPEED_PICKUP, SpeedPowerUpActive);
        EventManager.StopListening(EventTypes.PLAYER_SPEED_ENDED, SpeedPowerUpEnded);
    }

    void setDay() {
        dayNightCycle.setDayTime();
    }

    void setNight() {
        dayNightCycle.setNightTime();
    }

    void SpeedPowerUpActive() {
        playerControls.SetShield(true);
    }

    void SpeedPowerUpEnded() {
        playerControls.SetShield(false);
    }
}
