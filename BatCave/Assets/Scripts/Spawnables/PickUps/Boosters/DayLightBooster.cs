using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DayLightBooster : PowerUpController
{
    public float DayTimeUpTime;

    // Use this for initialization
    void Start()
    {
        EventManager.StartListening(EventTypes.SET_DAY_TIME, startCycle);
    }

    void startCycle()
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
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            EventManager.TriggerEvent(EventTypes.SET_DAY_TIME);
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
