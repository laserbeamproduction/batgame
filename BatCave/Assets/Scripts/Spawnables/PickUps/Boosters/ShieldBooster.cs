using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldBooster : PowerUpController
{
    public float shieldDuration;

    void Start()
    {
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_PICKUP, activateShield);
    }

    void activateShield(object arg0)
    {
        StartCoroutine(startCoolDown());
    }

    IEnumerator startCoolDown()
    {
        yield return new WaitForSeconds(shieldDuration);
        EventManager.TriggerEvent(EventTypes.PLAYER_SHIELD_ENDED);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            EventManager.TriggerEvent(EventTypes.PLAYER_SHIELD_PICKUP);
        }
    }
}
