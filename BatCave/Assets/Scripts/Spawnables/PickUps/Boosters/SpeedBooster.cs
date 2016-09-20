using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class SpeedBooster : PowerUpController
{

    public float speedDuration;

    void Start()
    {
        EventManager.StartListening(EventTypes.PLAYER_SPEED_PICKUP, activateSpeed);
    }

    void activateSpeed(object arg0)
    {
        StartCoroutine(startCoolDown());
    }

    IEnumerator startCoolDown()
    {
        yield return new WaitForSeconds(speedDuration);
        EventManager.TriggerEvent(EventTypes.PLAYER_SPEED_ENDED);
        StopCoroutine(startCoolDown());
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            EventManager.TriggerEvent(EventTypes.PLAYER_SPEED_PICKUP);
        }
    }
}
