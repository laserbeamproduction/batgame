using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SpeedBooster : PowerUpController
{

    public float speedDuration;

    void Start()
    {
        EventManager.StartListening(EventTypes.PLAYER_SPEED_PICKUP, activateSpeed);
    }

    void activateSpeed()
    {
        StartCoroutine(startCoolDown());
    }

    IEnumerator startCoolDown()
    {
        yield return new WaitForSeconds(speedDuration);
        EventManager.TriggerEvent(EventTypes.PLAYER_SPEED_ENDED);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            EventManager.TriggerEvent(EventTypes.PLAYER_SPEED_PICKUP);
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
