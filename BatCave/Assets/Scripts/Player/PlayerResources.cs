using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {
    public float health;
    public float maxHealth;
    public int echoUsedAmount;

    public int damageAmount;
    public int healthPickupAmount;
	
	// Update is called once per frame
	void Update () {
        if (health < 0) {
            health = 0;
        }

        if (health > maxHealth) {
            health = maxHealth;
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
}
