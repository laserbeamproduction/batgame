using UnityEngine;
using System.Collections.Generic;

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

    void setDay(object arg0) {
        dayNightCycle.setDayTime();
    }

    void setNight(object arg0) {
        dayNightCycle.setNightTime();
    }

    void SpeedPowerUpActive(object arg0) {
        dayNightCycle.IncreasePlayerLightRange();
        playerControls.SetShield(true);
    }

    void SpeedPowerUpEnded(object arg0) {
        dayNightCycle.DecreasePlayerLightRange();
        playerControls.SetShield(false);
    }
}
