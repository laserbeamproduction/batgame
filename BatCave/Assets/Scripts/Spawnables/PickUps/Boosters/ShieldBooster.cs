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
        EventManager.TriggerEvent(EventTypes.PLAYER_SHIELD_ENDED, null);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            EventManager.TriggerEvent(EventTypes.PLAYER_SHIELD_PICKUP, null);
        }

        if (col.gameObject.tag == "CleanUp")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.position = new Vector2(-10, 0);
        }

        if (col.gameObject.tag == "Obstacle")
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f);
        }
    }
}
