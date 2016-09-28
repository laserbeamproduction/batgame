using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSpecialAbility : MonoBehaviour {
    public float speedDuration;
    public float shieldDuration;
    public float DayTimeUpTime;

    public Image specialCycler;
    public GameObject[] boosts;
    public int cycleAmount;
    public int flickerAmount;

    private float cycleDelay;

    private void Start() {
        EventManager.StartListening(EventTypes.SPECIAL_USED, SpecialUsed);
    }

    private void SpecialUsed(object value) {
        specialCycler.enabled = true;
        cycleDelay = 0.05f;
        StartCoroutine(CycleSpritesDelay());
    }

    IEnumerator CycleSpritesDelay()
    {
        for (int i = 0; i < cycleAmount; i++)
        {
            for (int j = 0; j < boosts.Length; j++)
            {
                yield return new WaitForSeconds(cycleDelay);
                specialCycler.sprite = boosts[j].GetComponent<SpriteRenderer>().sprite;
            }
            cycleDelay += 0.05f;
        }

        GameObject boostToActivate = boosts[Random.Range(0, boosts.Length)];
        specialCycler.sprite = boostToActivate.GetComponent<SpriteRenderer>().sprite;

        for (int i = 0; i < flickerAmount; i++) {
            specialCycler.enabled = false;
            yield return new WaitForSeconds(0.1f);
            specialCycler.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        ActivateBooster(boostToActivate);

        yield return new WaitForSeconds(2);
        specialCycler.enabled = false;
        StopCoroutine(CycleSpritesDelay());
    }

    private void ActivateBooster(GameObject booster) {
        string boosterName = booster.name;

        switch (boosterName) {
            case "SpeedBooster":
                EventManager.TriggerEvent(PowerupEvents.PLAYER_SPEED_PICKUP, speedDuration);
                Debug.Log("Speed boost active");
                break;
            case "ShieldBooster":
                EventManager.TriggerEvent(PowerupEvents.PLAYER_SHIELD_PICKUP, shieldDuration);
                Debug.Log("Shield boost active");
                break;
            case "DayTimeBooster":
                EventManager.TriggerEvent(PowerupEvents.PLAYER_LIGHT_PICKUP, DayTimeUpTime);
                Debug.Log("Light boost active");
                break;
            default:
                Debug.Log("404 Booster not found!");
                break;
        }
    }
}
