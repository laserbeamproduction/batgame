using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {
    public float health;
    public float maxHealth;
    public int echoUsedAmount;

    public int damageAmount;
    public int healthPickupAmount;

    //Special resource
    public float maxEchoComboAmount;
    public float echoComboAmount;
    public int perfectEchoReward;
    public int goodEchoReward;

    private void Start() {
        EventManager.StartListening(EventTypes.SPECIAL_USED, RemoveSpecialResource);
    }

	// Update is called once per frame
	void Update () {
        if (health < 0) {
            health = 0;
        }

        if (health > maxHealth) {
            health = maxHealth;
        }

        if (echoComboAmount > maxEchoComboAmount) {
            echoComboAmount = maxEchoComboAmount;
        }

        if (echoComboAmount < 0) {
            echoComboAmount = 0;
        }
	}

    public void addHealth(int value) {
        health += value;
    }

    public void removeHealth(int value) {
        health -= value;
    }

    public void echoUsed() {
        echoUsedAmount += 1;
    }

    public void GoodEcho() {
        echoComboAmount += goodEchoReward;
    }

    public void PerfectEcho() {
        echoComboAmount += perfectEchoReward;
    }

    private void RemoveSpecialResource(object value) {
        echoComboAmount -= maxEchoComboAmount;
    }
}
