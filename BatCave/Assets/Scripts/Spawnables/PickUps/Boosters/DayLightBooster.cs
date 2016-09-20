using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayLightBooster : PowerUpController
{
    public float DayTimeUpTime;

    // Use this for initialization
    void Start()
    {
        EventManager.StartListening(EventTypes.SET_DAY_TIME, startCycle);
    }

    void startCycle(object arg0)
    {
        StartCoroutine(startCoolDown());
    }

    IEnumerator startCoolDown()
    {
        yield return new WaitForSeconds(DayTimeUpTime);
        EventManager.TriggerEvent(EventTypes.SET_NIGHT_TIME);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            EventManager.TriggerEvent(EventTypes.SET_DAY_TIME);
        }
    }
}
